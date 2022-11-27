using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace FoodRocket.Services.Inventory.Application
{
    public interface IIdentityContext
    {
        long Id { get; }
        public int OrganizationId { get; }

        public string UserType { get; set; }
        string Role { get; }
        bool IsAuthenticated { get; }
        bool IsAdmin { get; }
        public List<string> Permissions { get; }
        [JsonIgnore]
        public IEnumerable<Claim> Claims { get; }
    }
}