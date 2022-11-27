using FoodRocket.Services.Inventory.Core.Entities.Inventory;

namespace FoodRocket.Services.Inventory.Core.Exceptions;

public class InvalidOperationOnQuantityOfProductException : DomainException
{
    public override string Code { get; } = "invalid_operation_on_product_quantity";

    public InvalidOperationOnQuantityOfProductException(string message) : base($"message")
    {
    }
}