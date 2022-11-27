using System.Net;
using Convey.Auth;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Convey.WebApi.Requests;
using FoodRocket.Services.Identity.Application;
using FoodRocket.Services.Identity.Application.Commands;
using FoodRocket.Services.Identity.Application.DTO;
using FoodRocket.Services.Identity.Application.Queries;
using FoodRocket.Services.Identity.Application.Services;
using FoodRocket.Services.Identity.Application.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodRocket.Services.Identity.Api.Controllers;
[Route("")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[Authorize]
public class IdentityController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IRequestDispatcher _requestDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IAppContext _appContext;

    public IdentityController(IQueryDispatcher queryDispatcher,
        IRequestDispatcher requestDispatcher, ICommandDispatcher commandDispatcher, IAppContext appContext)
    {
        _queryDispatcher = queryDispatcher;
        _requestDispatcher = requestDispatcher;
        _commandDispatcher = commandDispatcher;
        _appContext = appContext;
    }

    /// <summary>
    /// Does sign in to the system.
    /// </summary>
    /// <returns>DTO with jwt and refresh token</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /sing-in
    ///     {
    ///        "email": "user1@mailinator.com",
    ///        "password": "AbcAbc$6"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">DTO with jwt and refresh token</response>
    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AuthDto))]
    public async Task<IActionResult> SignIn([FromBody] SignIn signIn)
    { 
        var authDto = await _requestDispatcher.DispatchAsync<SignIn, AuthDto>(signIn);
        return Ok(authDto);
    }

    /// <summary>
    /// Creates new user.
    /// </summary>
    /// <returns>Created status code</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /sign-up
    ///     {
    ///       "organizationId": 1,
    ///       "email": "user1@mailinator.com",
    ///       "password": "AbcAbc$6",
    ///       "userType": "client | customer",
    ///       "firstName" : "",
    ///       "lastName" : "",
    ///       "role": "admin | manager | waiter",
    ///       "permissions" : []
    ///      }
    /// </remarks>
    /// <response code="201">New user was created</response>
    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerResponse(StatusCodes.Status201Created)]
    public async Task<IActionResult> SignUp([FromBody] SignUp signUp)
    {
         await _commandDispatcher.SendAsync(signUp);
    
        return Created($"me",null);
    }
    
    /// <summary>
    /// Fetches logged in user details.
    /// </summary>
    /// <returns>Currently logged in user details</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /me
    ///
    /// </remarks>
    /// <response code="200">Returns logged in user details DTO</response>
    /// <response code="401">Not authenticated request</response>
    [HttpGet("me")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Me()
    {
        var user = await _queryDispatcher.QueryAsync<GetUser, UserDto>(new GetUser { UserId =_appContext.Identity.Id});
        return Ok(user);
    }

    /// <summary>
    /// Revokes access token of currently assigned user.
    /// </summary>
    /// <returns>Only OK status code</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /access-tokens/revoke
    ///    {
    ///       accessToken: some_jwt>
    ///    }
    /// </remarks>
    /// <response code="200">Returns logged in user details DTO</response>
    /// <response code="401">Not authenticated request</response>
    [HttpPost("access-tokens/revoke")]
    public async Task<IActionResult> RevokeAccessToken([FromBody] RevokeAccessToken revokeAccessToken, [FromServices] IAccessTokenService accessTokenService)
    {
        await accessTokenService.DeactivateAsync(revokeAccessToken.AccessToken);
        return Ok();
    }
    //
    // [HttpPost("refresh-tokens/revoke")]
    // public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshToken revokeRefreshToken)
    // {
    //     await _commandDispatcher.SendAsync(revokeRefreshToken);
    //     return Ok(Response.StatusCode = 204);
    // } 
    //
    // [HttpPost("refresh-tokens/use")]
    // public async Task<IActionResult> UseRefreshToken([FromBody] UseRefreshToken useRefreshToken)
    // {
    //     await _requestDispatcher.DispatchAsync<UseRefreshToken,AuthDto>(useRefreshToken);
    //     return Created($"refresh-tokens/use", null);
    // }
    //
    // [HttpPost("invite/user")]
    // public async Task<IActionResult> Invite([FromBody] InviteUser inviteUser)
    // {
    //     var id = await _requestDispatcher.DispatchAsync<InviteUser, bool>(inviteUser);
    //     return Ok(id);
    // }
    // [HttpPost("invite/accept")]
    // public async Task<IActionResult> AcceptInvitation([FromBody] AcceptUserInvitaion acceptInvitation)
    // {
    //     await _commandDispatcher.SendAsync<AcceptUserInvitaion>(acceptInvitation);
    //     return Ok();
    // }
    //
    // [HttpGet("user")]
    // public async Task<IActionResult> GetUser([FromQuery] GetUser getUser)
    // {
    //     var user = await _queryDispatcher.QueryAsync<GetUser, UserDto>(getUser);
    //     return Ok(user);
    // }
    // [HttpGet("user/confirm-email")]
    // public async Task<IActionResult> ConfirmEmail([FromQuery]ConfirmEmailToken confirmEmail)
    // {
    //     var a= await _requestDispatcher.DispatchAsync<ConfirmEmailToken, ConfirmEmailDto>(confirmEmail);
    //     return Ok(a);
    // }
    //
    // [HttpGet("roles")]
    // [AllowAnonymous]
    // public async Task<IActionResult> GetRoles()
    // {
    //     var roles = await _queryDispatcher.QueryAsync<GetRoles, List<RoleDto>>(new GetRoles());
    //     return Ok(roles);
    // }
    // [HttpGet("roles/{Id}")]
    // [AllowAnonymous]
    // public async Task<IActionResult> GetRole([FromRoute]GetRole role)
    // {
    //     var roles = await _queryDispatcher.QueryAsync<GetRole, RoleDto>(role);
    //     return Ok(roles);
    // }
    // [HttpPost("roles")]
    // [AllowAnonymous]
    // public async Task<IActionResult> GetRole([FromBody]CreateRole role)
    // {
    //     var roles = await _requestDispatcher.DispatchAsync<CreateRole, Guid>(role);
    //     return Ok(roles);
    // }
    // [HttpPost("forgot-password")]
    // [AllowAnonymous]
    // public async Task<IActionResult> ForgotPassword(ForgotPassword command)
    // {
    //     await _commandDispatcher.SendAsync<ForgotPassword>(command);
    //     return Ok();
    // }
    // [HttpPost("reset-password")]
    // [AllowAnonymous]
    // public async Task<IActionResult> ResetPassword(ResetPassword command)
    // {
    //     await _commandDispatcher.SendAsync<ResetPassword>(command);
    //     return Ok();
    // }
    
    // private static async Task GetUserAsync(long id, HttpContext context)
    // {
    //     var user = await context.RequestServices.GetService<IIdentityService>().GetAsync(id);
    //     if (user is null)
    //     {
    //         context.Response.StatusCode = 404;
    //         return;
    //     }
    //
    //     await context.Response.WriteJsonAsync(user);
    // }
}
