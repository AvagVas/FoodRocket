namespace FoodRocket.Services.Inventory.Core.Exceptions;

public class InvalidUnitOfMeasureException : DomainException
{
    public override string Code { get; } = "invalid_unit_of_measure";
        
    public InvalidUnitOfMeasureException() : base($"Invalid data for unit of measure.")
    {
    }
}