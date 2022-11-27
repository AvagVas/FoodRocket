using Convey.CQRS.Commands;

namespace FoodRocket.Services.Identity.Application.Commands;

public class RevokeRefreshToken : ICommand
{
    public string RefreshToken { get; }

    public RevokeRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}
