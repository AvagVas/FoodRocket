using FoodRocket.Services.Inventory.Core.Entities.Customers;

namespace FoodRocket.Services.Inventory.Core.Events.Customers;

public class ShippingAddressUpdated : IDomainEvent
{
    public Customer Customer { get; }

    public ShippingAddressUpdated(Customer customer)
        => Customer = customer;
}
