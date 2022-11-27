using System.Security.Claims;
using System.Text.Json.Serialization;
using FoodRocket.Services.Inventory.Application;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using Microsoft.AspNetCore.Http;

namespace FoodRocket.Services.Inventory.Infrastructure.Contexts;

internal class IdentityContext : IIdentityContext
{
    public long Id { get; }
    public int OrganizationId { get; }
    public string UserType { get; set; }
    public string Role { get; } = string.Empty;
    public bool IsAuthenticated { get; }
    public bool IsAdmin { get; }
    public List<string> Permissions { get; }
    [JsonIgnore]
    public IEnumerable<Claim> Claims { get; } = new List<Claim>();

    internal IdentityContext()
    {
    }

    internal IdentityContext(CorrelationContext.UserContext context)
        : this(context.Id, context.UserType, context.Role, context.IsAuthenticated, context.Claims)
    {
    }
    internal IdentityContext(HttpContext context) : this(context.User)
    {
    }
    internal IdentityContext(ClaimsPrincipal userClaim)
    {
        Id = long.TryParse(userClaim.Identity.Name, out var userId) ? userId : 0;
        OrganizationId = userClaim.Claims.Where(x => x.Type == "organizationId").Select(x =>
        {
            return Convert.ToInt32(x.Value);
        }).FirstOrDefault();

        UserType = userClaim.Claims.Where(x => x.Type == "userType").Select(x =>
        {
            return x.Value;
        }).FirstOrDefault();
        
        Role = ((ClaimsIdentity)userClaim.Identity).Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .FirstOrDefault().Value;
        IsAuthenticated = userClaim.Identity.IsAuthenticated;
        IsAdmin = Role.Equals("admin", StringComparison.InvariantCultureIgnoreCase);
        
        Permissions = userClaim.Claims
            .Where(x => x.Type == "permissions")
            .Select(x => { return x.Value; })
            .ToList();
        Claims = userClaim.Claims;
    }
    internal IdentityContext(string id, string userType, string role, bool isAuthenticated, IDictionary<string, string> claims)
    {
        Id = long.TryParse(id, out var userId) ? userId : 0;
        Role = role ?? string.Empty;
        IsAuthenticated = isAuthenticated;
        IsAdmin = Role.Equals("admin", StringComparison.InvariantCultureIgnoreCase);
        UserType = userType;
        //Claims = claims ?? new Dictionary<string, string>();
    }
        
    internal static IIdentityContext Empty => new IdentityContext();
}
