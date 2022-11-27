using FoodRocket.Services.Inventory.Core.Entities.Customers;

namespace FoodRocket.Services.Inventory.Core.Events.Customers;

public class BillingAddressUpdated : IDomainEvent
{
    public Customer Customer { get; }

    public BillingAddressUpdated(Customer customer)
        => Customer = customer;
}
