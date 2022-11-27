using FoodRocket.Services.Inventory.Core.Entities.Inventory;

namespace FoodRocket.Services.Inventory.Core.Exceptions;

public class UnitOfMeasuresMismatchException : DomainException
{
    public override string Code { get; } = "unit_of_measure_mismatch";
    public UnitOfMeasure UnitOfMeasure1 { get; }
    public UnitOfMeasure UnitOfMeasure2 { get; }
    
    public UnitOfMeasuresMismatchException(UnitOfMeasure unitOfMeasure1, UnitOfMeasure unitOfMeasure2) : base($"Units of measure are not compatible to be converted.")
    {
        UnitOfMeasure1 = unitOfMeasure1;
        UnitOfMeasure2 = unitOfMeasure2;
    }
}