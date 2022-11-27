using Convey.CQRS.Events;
using Convey.MessageBrokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Application.Events.External
{
    [Message("identity", routingKey: "send_email")]
    public class SendEmail : IEvent
    {
        public string From { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public SendEmail(){}
    }
}
