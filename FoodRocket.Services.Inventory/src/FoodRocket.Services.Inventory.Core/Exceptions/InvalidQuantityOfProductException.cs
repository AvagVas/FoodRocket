using FoodRocket.Services.Inventory.Core.Entities.Inventory;

namespace FoodRocket.Services.Inventory.Core.Exceptions;

public class InvalidQuantityOfProductException : DomainException
{
    public override string Code { get; } = "invalid_quantity_of_product";
    public Product Product { get; }
    public UnitOfMeasure UnitOfMeasure { get; }
    public decimal Quantity { get; }
    
    public InvalidQuantityOfProductException(Product product, UnitOfMeasure unitOfMeasure, decimal quantity) : base($"Invalid quantity is setup, see fields for details.")
    {
        Product = product;
        UnitOfMeasure = unitOfMeasure;
        Quantity = quantity;
    }
}