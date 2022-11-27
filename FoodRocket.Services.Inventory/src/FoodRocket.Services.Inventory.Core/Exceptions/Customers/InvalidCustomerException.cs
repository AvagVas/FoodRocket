using FoodRocket.Services.Inventory.Core.Exceptions;

namespace FoodRocket.Services.Inventory.Core.Exceptions.Customers;

public class InvalidCustomerException : DomainException
{
    public override string Code { get; } = "invalid_customer";

    public InvalidCustomerException(string message) : base($"Failed on creation: {message}")
    {
    }
}