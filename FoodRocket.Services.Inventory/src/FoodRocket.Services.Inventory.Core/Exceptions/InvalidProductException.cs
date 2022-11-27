namespace FoodRocket.Services.Inventory.Core.Exceptions;

public class InvalidProductException : DomainException
{
    public override string Code { get; } = "invalid_product";
        
    public InvalidProductException() : base($"Invalid product, failed on creation.")
    {
    }
}