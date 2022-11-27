using FoodRocket.Services.Identity.Application.DTO;

namespace FoodRocket.Services.Identity.Application.Services;

public interface IRefreshTokenService
{
    Task<string> CreateAsync(long userId);
    Task RevokeAsync(string refreshToken);
    Task<AuthDto> UseAsync(string refreshToken);
}
