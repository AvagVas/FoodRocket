using Convey;
using Convey.Logging.CQRS;
using Microsoft.Extensions.DependencyInjection;
using FoodRocket.Services.Inventory.Application.Commands;
using FoodRocket.Services.Inventory.Application.Commands.Inventory;

namespace FoodRocket.Services.Inventory.Infrastructure.Logging
{
    internal static class Extensions
    {
        public static IConveyBuilder AddHandlersLogging(this IConveyBuilder builder)
        {
            var assembly = typeof(AddProduct).Assembly;
            
            builder.Services.AddSingleton<IMessageToLogTemplateMapper>(new MessageToLogTemplateMapper());
            
            return builder
                .AddCommandHandlersLogging(assembly)
                .AddEventHandlersLogging(assembly);
        }
    }
}