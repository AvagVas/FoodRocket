using Convey.CQRS.Commands;
using FoodRocket.Services.Inventory.Application.Exceptions.Customers;
using FoodRocket.Services.Inventory.Application.Services;
using FoodRocket.Services.Inventory.Core.Entities.Customers;
using FoodRocket.Services.Inventory.Core.Repositories.Customers;

namespace FoodRocket.Services.Inventory.Application.Commands.Customers.Handlers;

public class AddCustomerHandler : ICommandHandler<AddCustomer>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IEventProcessor _eventProcessor;

    public AddCustomerHandler(ICustomerRepository customerRepository, IEventProcessor eventProcessor)
    {
        _customerRepository = customerRepository;
        _eventProcessor = eventProcessor;
    }

    public async Task HandleAsync(AddCustomer command, CancellationToken cancellationToken = new CancellationToken())
    {
        if (await _customerRepository.ExistsAsync(command.CustomerId))
        {
            throw new CustomerAlreadyExistsException(command.CustomerId);
        }

        var customer = Customer.Create(command.CustomerId, command.FirstName, command.LastName, command.IsActive);
        await _customerRepository.AddAsync(customer);
        await _eventProcessor.ProcessAsync(customer.Events);
    }
}