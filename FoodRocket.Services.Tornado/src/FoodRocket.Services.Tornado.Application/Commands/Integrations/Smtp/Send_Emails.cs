using Convey.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Application.Commands.Integrations.Smtp
{
    [Contract]
    public class Send_Emails : ICommand
    {
        public string From { get; set; }
        public List<string> Addresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
