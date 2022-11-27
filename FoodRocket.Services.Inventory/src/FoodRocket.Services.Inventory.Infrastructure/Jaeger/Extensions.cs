using Convey;
using Convey.CQRS.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace FoodRocket.Services.Inventory.Infrastructure.Jaeger
{
    internal static class Extensions
    {
        public static IConveyBuilder AddJaegerDecorators(this IConveyBuilder builder)
        {
            builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(JaegerCommandHandlerDecorator<>));

            return builder;
        }
    }
}