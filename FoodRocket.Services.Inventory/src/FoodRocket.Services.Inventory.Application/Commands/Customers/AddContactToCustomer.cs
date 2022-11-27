using Convey.CQRS.Commands;

namespace FoodRocket.Services.Inventory.Application.Commands.Customers;

public class AddContactToCustomer : ICommand
{
    public long CustomerId { get; set; } = 0;

    public string? Name { get; set; }
    public string? ContactType { get; set; }
    public string? Value { get; set; }
    public bool? SetAsPrimary { get; set; }
}