namespace FoodRocket.DBContext.Services;

public class IdGeneratorMisconfiguredException : Exception
{
    public string Code { get; } = "id_generator_misconfigured";

    public IdGeneratorMisconfiguredException() : base("Check ID generator configuration.")
    {
    }
}
