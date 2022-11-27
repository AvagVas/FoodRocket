using Convey.CQRS.Commands;
using Convey.WebApi.Requests;
using FoodRocket.Services.Tornado.Application.Commands.Integrations.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodRocket.Services.Tornado.Api.Controllers
{
    [Route("api/tornado")]
    [ApiController]
    [Consumes("application/json")]
    public class TornadoController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public TornadoController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("email")]
        public async Task<IActionResult> SendEmailAsync(Send_Email sendEmail)
        {
            await _commandDispatcher.SendAsync(sendEmail);
            return Ok();
        }

        [HttpPost("bulk/email")]
        public async Task<IActionResult> SendEmailBulkAsync(Send_Emails sendEmail)
        {
            await _commandDispatcher.SendAsync(sendEmail);
            return Ok();
        }
    }
}
