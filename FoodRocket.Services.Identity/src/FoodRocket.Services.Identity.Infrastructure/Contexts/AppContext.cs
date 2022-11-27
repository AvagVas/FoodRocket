using FoodRocket.Services.Identity.Infrastructure.Contexts;
using FoodRocket.Services.Identity.Application;

namespace FoodRocket.Services.Identity.Infrastructure.Contexts;

internal class AppContext : IAppContext
{
    public string RequestId { get; }
    public IIdentityContext Identity { get; }

    internal AppContext() : this(Guid.NewGuid().ToString("N"), IdentityContext.Empty)
    {
    }

    internal AppContext(CorrelationContext context) : this(context.CorrelationId,
        context.User is null ? IdentityContext.Empty : new IdentityContext(context.User))
    {
    }
    internal AppContext(Microsoft.AspNetCore.Http.HttpContext context) : this(Guid.NewGuid().ToString("N"),
        context.User.Identity.IsAuthenticated ? new IdentityContext(context) : IdentityContext.Empty)
    {
    }
    internal AppContext(string requestId, IIdentityContext identity)
    {
        RequestId = requestId;
        Identity = identity;
    }
        
    internal static IAppContext Empty => new AppContext();
}
