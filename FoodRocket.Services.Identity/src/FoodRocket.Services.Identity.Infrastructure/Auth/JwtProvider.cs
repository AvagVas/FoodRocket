using Convey.Auth;
using FoodRocket.Services.Identity.Application.DTO;
using FoodRocket.Services.Identity.Application.Services;
using FoodRocket.Services.Identity.Core.Entities;

namespace FoodRocket.Services.Identity.Infrastructure.Auth;

public class JwtProvider : IJwtProvider
{
    private readonly IJwtHandler _jwtHandler;

    public JwtProvider(IJwtHandler jwtHandler)
    {
        _jwtHandler = jwtHandler;
    }

    public AuthDto Create(long userId, UserType userType, string role, string audience = null,
        IDictionary<string, IEnumerable<string>> claims = null)
    {
        var jwt = _jwtHandler.CreateToken(userId.ToString(), role, audience, claims);

        return new AuthDto
        {
            AccessToken = jwt.AccessToken,
            UserType = userType.ToString().ToLower(),
            Role = jwt.Role,
            Expires = jwt.Expires
        };
    }
}
