namespace FoodRocket.Services.Inventory.Infrastructure.Exceptions;

public abstract class InfraException : Exception
{
    public virtual string Code { get; } = "infrastructure_related_error";

    protected InfraException(string message) : base(message)
    {
    }
}
