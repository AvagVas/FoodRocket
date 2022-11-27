using Convey.CQRS.Commands;
using FoodRocket.Services.Identity.Application.Services;

namespace FoodRocket.Services.Identity.Application.Commands.Handlers;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IIdentityService _identityService;

    public SignUpHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public Task HandleAsync(SignUp command, CancellationToken cancellationToken = new CancellationToken()) =>
        _identityService.SignUpAsync(command);
}
