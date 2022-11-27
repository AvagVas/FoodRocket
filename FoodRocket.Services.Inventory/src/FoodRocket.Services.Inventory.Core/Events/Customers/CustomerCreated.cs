using FoodRocket.Services.Inventory.Core.Entities.Customers;

namespace FoodRocket.Services.Inventory.Core.Events.Customers;

public class CustomerCreated : IDomainEvent
{
    public Customer Customer { get; }

    public CustomerCreated(Customer customer)
        => Customer = customer;
}
