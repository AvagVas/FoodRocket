using FoodRocket.Services.Tornado.Application.ValueObject;
using System;
using System.Text;

namespace FoodRocket.Services.Tornado.Application.Services
{
    public interface IEmailClient
    {
        Task<bool> SendEmailAsync(string subject,string body, string from, string to, string receiverName);

        Task<bool> SendEmailBasedOnTemplateAsync(string subject,StringBuilder template, object model, string from, string to, string receiverName);
        Task<bool> SendBulkEmailAsync(string subject,string body, string from,List<string> addresses);
    }
}
