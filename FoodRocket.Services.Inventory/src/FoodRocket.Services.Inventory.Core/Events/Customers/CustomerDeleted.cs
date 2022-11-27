using FoodRocket.Services.Inventory.Core.Entities.Customers;

namespace FoodRocket.Services.Inventory.Core.Events.Customers;

public class CustomerDeleted : IDomainEvent
{
    public Customer Customer { get; }

    public CustomerDeleted(Customer customer)
        => Customer = customer;
}