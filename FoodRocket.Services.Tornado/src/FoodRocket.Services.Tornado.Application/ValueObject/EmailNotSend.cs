using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Application.ValueObject
{
    public class EmailNotSend
    {
        public string Email { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
        public string Code { get; set; }
    }
}
