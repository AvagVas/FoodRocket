using System.Text.RegularExpressions;
using FoodRocket.Services.Identity.Application.Commands;
using FoodRocket.Services.Identity.Application.DTO;
using FoodRocket.Services.Identity.Application.Events;
using FoodRocket.Services.Identity.Application.Exceptions;
using FoodRocket.Services.Identity.Core.Entities;
using FoodRocket.Services.Identity.Core.Exceptions;
using FoodRocket.Services.Identity.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace FoodRocket.Services.Identity.Application.Services.Identity;

public class IdentityService : IIdentityService
{
    private static readonly Regex EmailRegex = new Regex(
        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<IdentityService> _logger;
    private readonly INewIdGenerator _idGenerator;

    public IdentityService(IUserRepository userRepository, IPasswordService passwordService,
        IJwtProvider jwtProvider, IRefreshTokenService refreshTokenService,
        IMessageBroker messageBroker, ILogger<IdentityService> logger, INewIdGenerator idGenerator)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jwtProvider = jwtProvider;
        _refreshTokenService = refreshTokenService;
        _messageBroker = messageBroker;
        _logger = logger;
        _idGenerator = idGenerator;
    }
    
    
    public async Task<UserDto> GetAsync(long id)
    {
        var user = await _userRepository.GetAsync(id);

        return user is null ? null : new UserDto(user);
    }

    public async Task<AuthDto> SignInAsync(SignIn command)
    {
        if (!EmailRegex.IsMatch(command.Email))
        {
            _logger.LogError($"Invalid email: {command.Email}");
            throw new InvalidEmailException(command.Email);
        }

        var user = await _userRepository.GetAsync(command.Email);
        if (user is null || !_passwordService.IsValid(user.Password, command.Password))
        {
            _logger.LogError($"User with email: {command.Email} was not found.");
            throw new InvalidCredentialsException(command.Email);
        }

        if (!_passwordService.IsValid(user.Password, command.Password))
        {
            _logger.LogError($"Invalid password for user with id: {user.Id.Value}");
            throw new InvalidCredentialsException(command.Email);
        }

        var claims = new Dictionary<string, IEnumerable<string>>();
        claims.Add("organizationId",new List<string>{ user.OrganizationId.ToString() });
        claims.Add("userType",new List<string>{ user.UserType.ToString().ToLower() });
        if (user.Permissions.Any())
        {
            claims.Add("permissions", user.Permissions);
        }
        var auth = _jwtProvider.Create(user.Id, user.UserType, user.Role, claims: claims);
        auth.RefreshToken = await _refreshTokenService.CreateAsync(user.Id);

        _logger.LogInformation($"User with id: {user.Id} has been authenticated.");
        await _messageBroker.PublishAsync(new SignedIn(user.Id, user.Role));

        return auth;
    }

    public async Task SignUpAsync(SignUp command)
    {
        if (!EmailRegex.IsMatch(command.Email))
        {
            _logger.LogError($"Invalid email: {command.Email}");
            throw new InvalidEmailException(command.Email);
        }

        var user = await _userRepository.GetAsync(command.Email);
        if (user is {})
        {
            _logger.LogError($"Email already in use: {command.Email}");
            throw new EmailInUseException(command.Email);
        }

        var role = string.IsNullOrWhiteSpace(command.Role) ? "user" : command.Role.ToLowerInvariant();
        var password = _passwordService.Hash(command.Password);
        if (!UserType.TryParse(command.UserType, true, out UserType userType))
        {
            throw new InvalidUserTypeValueException(command.UserType);
        }

        user = new User(_idGenerator.GetNewIdFor("user"), command.OrganizationId, command.Email, password, userType, role, DateTime.UtcNow, command.FirstName, command.LastName, command.Permissions);
        await _userRepository.AddAsync(user);
            
        _logger.LogInformation($"Created an account for the user with id: {user.Id}.");
        await _messageBroker.PublishAsync(new SignedUp(user.Id, user.Email, command.UserType, user.Role, user.FirstName, user.LastName));
    }
}
