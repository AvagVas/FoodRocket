using FoodRocket.Services.Inventory.Core.Entities.Inventory;

namespace FoodRocket.Services.Inventory.Core.Exceptions;

public class UnitOfMeasureCouldNotBeAddedToProductException : DomainException
{
    public override string Code { get; } = "unit_of_measure_cannot_be_added";
    public Product Product { get; }
    public UnitOfMeasure UnitOfMeasure { get; }
    public UnitOfMeasureCouldNotBeAddedToProductException(UnitOfMeasure unitOfMeasure, Product product) : base($"The unit of measure {unitOfMeasure.Name} cannot be added to product {product.Name}.")
    {
        Product = product;
        UnitOfMeasure = unitOfMeasure;
    }
}