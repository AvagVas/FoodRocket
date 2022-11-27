using Convey.WebApi.Requests;
using FoodRocket.Services.Identity.Application.DTO;
using FoodRocket.Services.Identity.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Identity.Application.Commands.Handlers
{
    public class SignInHandler : IRequestHandler<SignIn, AuthDto>
    {
        private readonly IIdentityService _identityService;

        public SignInHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<AuthDto> HandleAsync(SignIn command,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await _identityService.SignInAsync(command);

        }
    }
}
