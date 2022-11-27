using Convey.CQRS.Events;
using FoodRocket.Services.Tornado.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Application.Events.External.Handlers
{
    public class SendEmailHandler : IEventHandler<SendEmail>
    {
        private readonly IEmailClient _emailClient;

        public SendEmailHandler(IEmailClient emailClient)
        {
            _emailClient = emailClient;
        }
        public async Task HandleAsync(SendEmail ev, CancellationToken cancellationToken = default)
        {
            await _emailClient.SendEmailAsync(ev.Subject, ev.Body, ev.From, ev.To, "");
        }
    }
}
