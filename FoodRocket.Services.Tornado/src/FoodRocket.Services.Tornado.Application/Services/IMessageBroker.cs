using Convey.CQRS.Events;

namespace FoodRocket.Services.Tornado.Application.Services
{
    public interface IMessageBroker
    {
        Task PublishAsync(IEnumerable<IEvent> events);
        Task PublishAsync(params IEvent[] events);
    }
}
