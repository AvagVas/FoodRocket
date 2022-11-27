using Convey.CQRS.Events;
using FoodRocket.Services.Tornado.Application.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Application.Events.Rejected
{
    [Contract]
    public class SendEmailRejected : IRejectedEvent
    {
        public List<EmailNotSend> MessageNotSend { get; set; }
        public string Reason { get; set; }
        public string Code { get; set; }

        public string Type { get; set; }
        public SendEmailRejected(List<EmailNotSend> messageNotSend, string type, string reason, string code)
        {
            MessageNotSend = messageNotSend;
            Reason = reason;
            Code = code;
            Type = type;
        }
        public SendEmailRejected()
        {

        }
    }
}
