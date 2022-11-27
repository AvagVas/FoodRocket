using Convey.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Application.Events
{
    public class RecieveCreated : IEvent
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public RecieveCreated(string from, string to, string message, string type)
        {
            From = from;
            To = to;
            Message = message;
            Type = type;
        }

    }
}
