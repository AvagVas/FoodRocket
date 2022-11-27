using Convey.CQRS.Commands;

namespace FoodRocket.Services.Inventory.Application.Commands.Customers;

public class AddAddressToCustomer : ICommand
{
    public long CustomerId { get; set; } = 0;

    public string? Country { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? AddressLine { get; set; }
    public string? ZipCode { get; set; }
    public bool? setAsMainShippingAddress { get; set; }
    public bool? setAsMainBillingAddress { get; set; }
}