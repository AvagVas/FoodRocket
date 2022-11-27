namespace FoodRocket.Services.Inventory.Api.Routes;

public static class IdentityRoutes
{
    public static IEndpointRouteBuilder MapInventory(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllers();
        return endpoints;
    }
}