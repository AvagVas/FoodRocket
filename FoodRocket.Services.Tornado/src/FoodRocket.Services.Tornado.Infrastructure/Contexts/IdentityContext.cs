using FoodRocket.Services.Tornado.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FoodRocket.Services.Tornado.Infrastructure.Contexts
{
    internal sealed class IdentityContext : IIdentityContext
    {
        public Guid Id { get; }
        public int OrganizationId { get; set; }
        public string Role { get; } = string.Empty;
        public bool IsAuthenticated { get; }
        public bool IsAdmin { get; }
        public List<string> Permissions { get; }
        public IEnumerable<Claim> Claims { get; } = new List<Claim>();

        internal IdentityContext()
        {
        }
        internal IdentityContext(HttpContext context) : this(context.User)
        {
        }
        internal IdentityContext(ClaimsPrincipal UserCliem)
        {
            Id = Guid.TryParse(UserCliem.Identity.Name, out var userId) ? userId : Guid.Empty;
            OrganizationId = UserCliem.Claims.Where(x => x.Type == "organizationId").Select(x =>
            {
                return Convert.ToInt32(x.Value);
            }).FirstOrDefault();

            Role = ((ClaimsIdentity)UserCliem.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .FirstOrDefault().Value;
            IsAuthenticated = UserCliem.Identity.IsAuthenticated;
            IsAdmin = Role.Equals("admin", StringComparison.InvariantCultureIgnoreCase);
            Permissions = UserCliem.Claims
                .Where(x => x.Type == "permissions")
                .Select(x => { return x.Value; })
                .ToList();
            Claims = UserCliem.Claims;
        }
        internal IdentityContext(CorrelationContext.UserContext context)
            : this(context.Id, context.Role, context.IsAuthenticated, context.Claims)
        {
        }

        internal IdentityContext(string id, string role, bool isAuthenticated, IDictionary<string, string> claims)
        {
            Id = Guid.TryParse(id, out var userId) ? userId : Guid.Empty;
            Role = role ?? string.Empty;
            IsAuthenticated = isAuthenticated;
            IsAdmin = Role.Equals("admin", StringComparison.InvariantCultureIgnoreCase);
            //Claims = claims ?? new Dictionary<string, string>();
        }

        internal static IIdentityContext Empty => new IdentityContext();
    }
}
