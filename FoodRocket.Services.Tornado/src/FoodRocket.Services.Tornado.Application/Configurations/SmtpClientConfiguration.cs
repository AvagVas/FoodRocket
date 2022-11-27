using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Application.Configurations
{
    public class SmtpClientConfiguration
    {
        public string MailFrom { get; set; }
        public string MailSmtpHost { get; set; }
        public int MailPort { get; set; }
        public string MailUser { get; set; }
        public string MailPassword { get; set; }
        public string SupportFrom { get; set; }
        public string SupportSmtpHost { get; set; }
        public int SupportPort { get; set; }
        public string SupportUser { get; set; }
        public string SupportPassword { get; set; }
    }
}
