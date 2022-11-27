using FoodRocket.Services.Tornado.Application.ValueObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Application.Exceptions.Integrations
{
    public class SendEmailException : AppException
    {
        public override string Code { get; } = "send_email";

        public List<EmailNotSend> EmailNotSend { get; set; }
        public SendEmailException(List<EmailNotSend> emailNotSend) : base(JsonConvert.SerializeObject(emailNotSend))
        {
            EmailNotSend = emailNotSend;
        }
    }
}
