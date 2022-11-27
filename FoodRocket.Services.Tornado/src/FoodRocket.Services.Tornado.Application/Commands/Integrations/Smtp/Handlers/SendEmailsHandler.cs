using Convey.CQRS.Commands;
using FoodRocket.Services.Tornado.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Application.Commands.Integrations.Smtp.Handlers
{
    internal class SendEmailsHandler : ICommandHandler<Send_Emails>
    {
        private readonly IEmailClient _emailClient;

        public SendEmailsHandler(IEmailClient emailClient)
        {
            _emailClient = emailClient;
        }
        public async Task HandleAsync(Send_Emails command, CancellationToken cancellationToken = default)
        {
            await _emailClient.SendBulkEmailAsync(command.Subject, command.Body, command.From, command.Addresses);
        }
    }
}
