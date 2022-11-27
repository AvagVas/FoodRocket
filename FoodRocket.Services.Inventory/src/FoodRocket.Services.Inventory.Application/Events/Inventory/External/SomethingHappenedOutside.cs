using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace FoodRocket.Services.Inventory.Application.Events.Inventory.External;

[Message("some_exchange")]
public class SomethingHappenedOutside : IEvent
{
    public int SomeId { get; }

    public SomethingHappenedOutside(int id)
    {
        SomeId = id;
    }
}