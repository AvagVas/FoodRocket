using Convey.CQRS.Commands;
using FoodRocket.Services.Inventory.Application.Exceptions.Customers;
using FoodRocket.Services.Inventory.Application.Services;
using FoodRocket.Services.Inventory.Core.Entities.Customers;
using FoodRocket.Services.Inventory.Core.Repositories.Customers;

namespace FoodRocket.Services.Inventory.Application.Commands.Customers.Handlers;

public class AddContactToCustomerHandler : ICommandHandler<AddContactToCustomer>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IEventProcessor _eventProcessor;
    private readonly INewIdGenerator _idGenerator;

    public AddContactToCustomerHandler(ICustomerRepository customerRepository, IEventProcessor eventProcessor, INewIdGenerator idGenerator)
    {
        _customerRepository = customerRepository;
        _eventProcessor = eventProcessor;
        _idGenerator = idGenerator;
    }

    public async Task HandleAsync(AddContactToCustomer command, CancellationToken cancellationToken = new CancellationToken())
    {
        var customerToBeUpdated = await _customerRepository.GetAsync(command.CustomerId);
        if (customerToBeUpdated is null)
        {
            throw new CustomerNotFoundException(command.CustomerId);
        }

        if (!Enum.TryParse<ContactType>(command.ContactType, true, out ContactType contactType))
        {
            throw new InvalidContactTypeException(command.ContactType);
        }

        Contact newContact = new(
            _idGenerator.GetNewIdFor("contact"),
            contactType,
            command!.Name!,
            command!.Value!,
            true );

        customerToBeUpdated.AddContact(newContact);
        if (command.SetAsPrimary.HasValue)
        {
            customerToBeUpdated.SetContactAsPrimary(newContact);
        }

        await _customerRepository.UpdateAsync(customerToBeUpdated);
        await _eventProcessor.ProcessAsync(customerToBeUpdated.Events);
    } 
}