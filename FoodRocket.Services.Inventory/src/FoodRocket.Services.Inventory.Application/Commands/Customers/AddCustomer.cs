using Convey.CQRS.Commands;

namespace FoodRocket.Services.Inventory.Application.Commands.Customers;

public class AddCustomer : ICommand
{
    public long CustomerId { get; set; } = 0;
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public bool IsActive { get; set; }
}