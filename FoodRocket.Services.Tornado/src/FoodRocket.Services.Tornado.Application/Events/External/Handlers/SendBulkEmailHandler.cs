using Convey.CQRS.Events;
using FoodRocket.Services.Tornado.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Application.Events.External.Handlers
{
    public class SendBulkEmailHandler : IEventHandler<SendBulkEmail>
    {
        private readonly IEmailClient _emailClient;

        public SendBulkEmailHandler(IEmailClient emailClient)
        {
            _emailClient = emailClient;
        }
        public Task HandleAsync(SendBulkEmail ev, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
            // await _emailClient.SendEmailsAsync(ev.s, ev.Body, ev.Password, ev.From, ev.To);
        }
    }
}
