using Convey.MessageBrokers.RabbitMQ;
using FoodRocket.Services.Tornado.Application.Commands.Integrations.Smtp;
using FoodRocket.Services.Tornado.Application.Enums;
using FoodRocket.Services.Tornado.Application.Events.External;
using FoodRocket.Services.Tornado.Application.Events.Rejected;
using FoodRocket.Services.Tornado.Application.Exceptions.Integrations;
using FoodRocket.Services.Tornado.Application.ValueObject;
using Newtonsoft.Json;

namespace FoodRocket.Services.Tornado.Infrastructure.Exceptions
{
    internal sealed class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return exception switch
            {
                SendEmailException ex => message switch
                {
                    SendEmail ev => new SendEmailRejected(JsonConvert.DeserializeObject<List<EmailNotSend>>(exception.Message), nameof(ChannelTypes.Email), ex.Message, ex.Code),
                    SendBulkEmail => new SendEmailRejected(JsonConvert.DeserializeObject<List<EmailNotSend>>(exception.Message), nameof(ChannelTypes.Email), ex.Message, ex.Code),
                    SendEmailRejected ev => new SendEmailRejected(JsonConvert.DeserializeObject<List<EmailNotSend>>(exception.Message), nameof(ChannelTypes.Email), ex.Message, ex.Code),
                    _ => null
                },
                _ => null
            };
#pragma warning restore CS8604 // Possible null reference argument.
        }

    }
}
