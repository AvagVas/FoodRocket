namespace FoodRocket.Services.Tornado.Api.Routes
{
    public static class TornadoRoutes
    {
        public static IEndpointRouteBuilder MapTornado(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllers();
            return endpoints;
        }
    }
}
