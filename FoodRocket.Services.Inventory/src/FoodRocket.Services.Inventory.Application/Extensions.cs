using System.Runtime.CompilerServices;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;

namespace FoodRocket.Services.Inventory.Application
{
    public static class Extensions
    {
        public static IConveyBuilder AddApplication(this IConveyBuilder builder)
            => builder
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryEventDispatcher();
    }
}