using Convey.CQRS.Events;
using Convey.MessageBrokers;
using FoodRocket.Services.Tornado.Application.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Application.Events.External
{
   // [Message("channels", routingKey: "send_bulk_email")]
    public class SendBulkEmail : IEvent
    {
        public List<SendEmailItem> SendEmailItems { get; set; }
    }
}
