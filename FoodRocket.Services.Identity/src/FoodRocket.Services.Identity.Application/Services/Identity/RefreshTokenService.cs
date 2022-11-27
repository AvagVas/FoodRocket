using FoodRocket.Services.Identity.Application.DTO;
using FoodRocket.Services.Identity.Application.Exceptions;
using FoodRocket.Services.Identity.Core.Entities;
using FoodRocket.Services.Identity.Core.Exceptions;
using FoodRocket.Services.Identity.Core.Repositories;

namespace FoodRocket.Services.Identity.Application.Services.Identity;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRng _rng;
    private readonly INewIdGenerator _idGenerator;

    public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository,
        IUserRepository userRepository, IJwtProvider jwtProvider, IRng rng, INewIdGenerator idGenerator)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _rng = rng;
        _idGenerator = idGenerator;
    }
    
    public async Task<string> CreateAsync(long userId)
    {
        var token = _rng.Generate(30, true);
        var refreshToken = new RefreshToken(_idGenerator.GetNewIdFor("refreshToken"), userId, token, DateTime.UtcNow);
        await _refreshTokenRepository.AddAsync(refreshToken);

        return token;
    }

    public async Task RevokeAsync(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetAsync(refreshToken);
        if (token is null)
        {
            throw new InvalidRefreshTokenException();
        }

        token.Revoke(DateTime.UtcNow);
        await _refreshTokenRepository.UpdateAsync(token);
    }

    public async Task<AuthDto> UseAsync(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetAsync(refreshToken);
        if (token is null)
        {
            throw new InvalidRefreshTokenException();
        }

        if (token.Revoked)
        {
            throw new RevokedRefreshTokenException();
        }

        var user = await _userRepository.GetAsync(token.UserId);
        if (user is null)
        {
            throw new UserNotFoundException(token.UserId);
        }

        var claims = user.Permissions.Any()
            ? new Dictionary<string, IEnumerable<string>>
            {
                ["permissions"] = user.Permissions
            }
            : null;
        var auth = _jwtProvider.Create(token.UserId, user.UserType, user.Role, claims: claims);
        auth.RefreshToken = refreshToken;

        return auth;
    }
}
