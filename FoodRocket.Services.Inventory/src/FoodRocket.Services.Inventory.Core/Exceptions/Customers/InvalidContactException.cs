using FoodRocket.Services.Inventory.Core.Exceptions;

namespace FoodRocket.Services.Inventory.Core.Exceptions.Customers;

public class InvalidContactException : DomainException
{
    public override string Code { get; } = "invalid_contact";

    public InvalidContactException(string message) : base($"Failed on creation: {message}")
    {
    }
}