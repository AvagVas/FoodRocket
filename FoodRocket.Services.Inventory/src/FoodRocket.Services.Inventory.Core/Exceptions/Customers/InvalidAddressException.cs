using FoodRocket.Services.Inventory.Core.Exceptions;

namespace FoodRocket.Services.Inventory.Core.Exceptions.Customers;

public class InvalidAddressException : DomainException
{
    public override string Code { get; } = "invalid_address";

    public InvalidAddressException(string message) : base($"Failed on creation: {message}")
    {
    }
}