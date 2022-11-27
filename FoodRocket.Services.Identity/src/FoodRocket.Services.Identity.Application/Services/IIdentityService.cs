using FoodRocket.Services.Identity.Application.Commands;
using FoodRocket.Services.Identity.Application.DTO;

namespace FoodRocket.Services.Identity.Application.Services;

public interface IIdentityService
{
    Task<UserDto> GetAsync(long id);
    Task<AuthDto> SignInAsync(SignIn command);
    Task SignUpAsync(SignUp command);
}
