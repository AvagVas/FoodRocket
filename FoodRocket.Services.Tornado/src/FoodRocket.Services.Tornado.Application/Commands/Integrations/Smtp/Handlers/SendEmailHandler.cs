using Convey.CQRS.Commands;
using FoodRocket.Services.Tornado.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Application.Commands.Integrations.Smtp.Handlers
{
    public class SendEmailHandler : ICommandHandler<Send_Email>
    {
        private readonly IEmailClient _emailClient;

        public SendEmailHandler(IEmailClient emailClient)
        {
          _emailClient = emailClient;
        }
        public async Task HandleAsync(Send_Email command, CancellationToken cancellationToken = default)
        {
           await _emailClient.SendEmailAsync(command.Subject,command.Body, command.From,command.To, "");
        }
    }
}
