using Convey.CQRS.Events;
using FoodRocket.Services.Tornado.Application.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Application.Events
{
    public class SentEmail : IEvent
    {
        public List<EmailSentItem> EmailSent { get; set; }
        public SentEmail(List<EmailSentItem> emailSent)
        {
            EmailSent = emailSent;
        }

        public SentEmail() { }
    }
}
