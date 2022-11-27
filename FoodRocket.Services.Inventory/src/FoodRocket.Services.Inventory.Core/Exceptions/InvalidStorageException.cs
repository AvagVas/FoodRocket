namespace FoodRocket.Services.Inventory.Core.Exceptions;

public class InvalidStorageException : DomainException
{
    public override string Code { get; } = "invalid_storage";
        
    public InvalidStorageException() : base($"Invalid storage, failed on creation.")
    {
    }
}