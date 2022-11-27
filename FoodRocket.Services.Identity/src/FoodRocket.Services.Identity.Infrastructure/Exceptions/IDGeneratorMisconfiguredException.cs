using FoodRocket.Services.Identity.Infrastructure.Exceptions;

namespace FoodRocket.Services.Identity.Infrastructure.Exceptions;

public class IdGeneratorMisconfiguredException : InfraException
{
    public override string Code { get; } = "id_generator_misconfigured";

    public IdGeneratorMisconfiguredException() : base("Check ID generator configuration.")
    {
    }
}
