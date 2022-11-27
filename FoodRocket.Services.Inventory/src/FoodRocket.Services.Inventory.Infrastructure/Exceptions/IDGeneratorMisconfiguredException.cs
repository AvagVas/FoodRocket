using FoodRocket.Services.Inventory.Infrastructure.Exceptions;

namespace FoodRocket.Services.Inventory.Infrastructure.Exceptions;

public class IdGeneratorMisconfiguredException : InfraException
{
    public override string Code { get; } = "id_generator_misconfigured";

    public IdGeneratorMisconfiguredException() : base("Check ID generator configuration.")
    {
    }
}
