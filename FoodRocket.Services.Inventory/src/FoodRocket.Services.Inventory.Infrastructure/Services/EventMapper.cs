using System.Collections.Generic;
using System.Linq;
using Convey.CQRS.Events;
using FoodRocket.Services.Inventory.Application.Events;
using FoodRocket.Services.Inventory.Application.Services;
using FoodRocket.Services.Inventory.Core.Events;


namespace FoodRocket.Services.Inventory.Infrastructure.Services
{
    internal sealed class EventMapper : IEventMapper
    {
        public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
            => events.Select(Map);

        public IEvent Map(IDomainEvent @event)
            => (@event switch
            {
                //TODO: Here will be mapping between domain events to integration events (application events)
                _ => null
            })!;
    }
}