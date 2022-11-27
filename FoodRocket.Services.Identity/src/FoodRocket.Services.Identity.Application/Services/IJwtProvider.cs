using FoodRocket.Services.Identity.Application.DTO;
using FoodRocket.Services.Identity.Core.Entities;

namespace FoodRocket.Services.Identity.Application.Services;

public interface IJwtProvider
{
    AuthDto Create(long userId, UserType userType, string role, string audience = null,
        IDictionary<string, IEnumerable<string>> claims = null);
}
