using FoodRocket.Services.Inventory.Core.Entities.Customers;

namespace FoodRocket.Services.Inventory.Core.Events.Customers;

public class CustomerUpdated : IDomainEvent
{
    public Customer Customer { get; }

    public CustomerUpdated(Customer customer)
        => Customer = customer;
}
