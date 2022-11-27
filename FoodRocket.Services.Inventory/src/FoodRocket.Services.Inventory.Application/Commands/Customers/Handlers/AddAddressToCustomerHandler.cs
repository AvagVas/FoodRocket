using Convey.CQRS.Commands;
using FoodRocket.Services.Inventory.Application.Exceptions.Customers;
using FoodRocket.Services.Inventory.Application.Services;
using FoodRocket.Services.Inventory.Core.Entities.Customers;
using FoodRocket.Services.Inventory.Core.Repositories.Customers;

namespace FoodRocket.Services.Inventory.Application.Commands.Customers.Handlers;

public class AddAddressToCustomerHandler : ICommandHandler<AddAddressToCustomer>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IEventProcessor _eventProcessor;
    private readonly INewIdGenerator _idGenerator;

    public AddAddressToCustomerHandler(ICustomerRepository customerRepository, IEventProcessor eventProcessor, INewIdGenerator idGenerator)
    {
        _customerRepository = customerRepository;
        _eventProcessor = eventProcessor;
        _idGenerator = idGenerator;
    }

    public async Task HandleAsync(AddAddressToCustomer command, CancellationToken cancellationToken = new CancellationToken())
    {
        var customerToBeUpdated = await _customerRepository.GetAsync(command.CustomerId);
        if (customerToBeUpdated is null)
        {
            throw new CustomerNotFoundException(command.CustomerId);
        }

        Address newAddress = new Address(_idGenerator.GetNewIdFor("address"), command.AddressLine, command.Country,command.State, command.City, command.ZipCode, true );

        customerToBeUpdated.AddAddress(newAddress);
        if (command.setAsMainBillingAddress.HasValue)
        {
            customerToBeUpdated.ChangeBillingAddress(newAddress);
        }

        if (command.setAsMainShippingAddress.HasValue)
        {
            customerToBeUpdated.ChangeShippingAddress(newAddress);
        }

        await _customerRepository.UpdateAsync(customerToBeUpdated);
        await _eventProcessor.ProcessAsync(customerToBeUpdated.Events);
    }


}