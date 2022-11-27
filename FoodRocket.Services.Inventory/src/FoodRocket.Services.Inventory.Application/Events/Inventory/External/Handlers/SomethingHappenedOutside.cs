using System.Threading.Tasks;
using Convey.CQRS.Events;

namespace FoodRocket.Services.Inventory.Application.Events.Inventory.External.Handlers
{
    public class SomethingHappenedOutsideHandler : IEventHandler<SomethingHappenedOutside>
    {
        public Task HandleAsync(SomethingHappenedOutside @event, CancellationToken cancellationToken = new CancellationToken())
            => Task.CompletedTask;

    }
}