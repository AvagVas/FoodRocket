using Convey.CQRS.Events;

namespace FoodRocket.Services.Identity.Application.Services;

public interface IMessageBroker
{
    Task PublishAsync(params IEvent[] events);
}
