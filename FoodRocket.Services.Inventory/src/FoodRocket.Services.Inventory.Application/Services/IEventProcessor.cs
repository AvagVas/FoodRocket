using System.Collections.Generic;
using System.Threading.Tasks;
using FoodRocket.Services.Inventory.Core.Events;

namespace FoodRocket.Services.Inventory.Application.Services
{
    public interface IEventProcessor
    {
        Task ProcessAsync(IEnumerable<IDomainEvent> events);
    }
}