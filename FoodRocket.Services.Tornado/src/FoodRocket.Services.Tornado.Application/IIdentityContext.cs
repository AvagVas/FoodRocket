using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace FoodRocket.Services.Tornado.Application
{
    public interface IIdentityContext
    {
        Guid Id { get; }
        public int OrganizationId { get; set; }
        string Role { get; }
        bool IsAuthenticated { get; }
        bool IsAdmin { get; }
        public List<string> Permissions { get; }
        IEnumerable<Claim> Claims { get; }
    }
}
