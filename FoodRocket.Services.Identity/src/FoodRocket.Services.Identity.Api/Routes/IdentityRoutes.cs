namespace FoodRocket.Services.Identity.Api.Routes;

public static class IdentityRoutes
{
    public static IEndpointRouteBuilder MapIdentity(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllers();
        return endpoints;
    }
}