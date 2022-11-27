namespace FoodRocket.Services.Inventory.Core.Exceptions;

public class InvalidProductNameException : DomainException
{
    public override string Code { get; } = "invalid_product_name";
    public string ProductName { get; }
    public InvalidProductNameException(string productName) : base($"Invalid product name: {productName}.")
    {
        ProductName = productName;
    }
}