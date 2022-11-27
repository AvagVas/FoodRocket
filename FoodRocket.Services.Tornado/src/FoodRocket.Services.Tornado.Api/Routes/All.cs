using Convey.Types;
using Convey.WebApi;
using Convey.WebApi.CQRS;

namespace FoodRocket.Services.Tornado.Api.Routes
{
    public static class All
    {
        public static void UseAllRoutes(this IApplicationBuilder app)
        {
          
            app.UseRouting();
            app.UseDispatcherEndpoints(endpoints => endpoints
                .Get("", ctx => ctx.Response.WriteAsync(ctx.RequestServices.GetService<AppOptions>().Name)));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapTornado();
            });
        }
    }
}
