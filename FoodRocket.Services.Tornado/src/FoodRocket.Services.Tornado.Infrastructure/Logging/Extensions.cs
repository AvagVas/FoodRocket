using Convey;
using Convey.Logging.CQRS;
using FoodRocket.Services.Tornado.Application.Commands.Integrations.Smtp;
using FoodRocket.Services.Tornado.Application.Events.External;
using Microsoft.Extensions.DependencyInjection;

namespace FoodRocket.Services.Tornado.Infrastructure.Logging
{
    internal static class Extensions
    {
        public static IConveyBuilder AddHandlersLogging(this IConveyBuilder builder)
        {
            var assembly = typeof(Send_Email).Assembly;

            builder.Services.AddSingleton<IMessageToLogTemplateMapper>(new MessageToLogTemplateMapper());

            return builder
                .AddCommandHandlersLogging(assembly)
                .AddEventHandlersLogging(assembly);
        }
    }
}
