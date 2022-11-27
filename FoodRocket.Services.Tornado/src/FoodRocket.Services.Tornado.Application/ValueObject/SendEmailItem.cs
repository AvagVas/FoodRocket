using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRocket.Services.Tornado.Application.ValueObject
{
    public class SendEmailItem
    {
        public Guid MessageId { get; }
        public string From { get; set; }
        public string Msg { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }

        public SendEmailItem()
        {
            ToAddresses = new List<EmailAddress>();
            FromAddresses = new List<EmailAddress>();
        }

        public List<EmailAddress> ToAddresses { get; set; }
        public List<EmailAddress> FromAddresses
        {
            get; set;
        }
    }
    public class EmailAddress
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
