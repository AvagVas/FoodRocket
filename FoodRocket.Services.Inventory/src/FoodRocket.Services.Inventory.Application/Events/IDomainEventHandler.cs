using System.Threading.Tasks;
using FoodRocket.Services.Inventory.Core.Events;

namespace FoodRocket.Services.Inventory.Application.Events
{
    public interface IDomainEventHandler<in T> where T : class, IDomainEvent
    {
        Task HandleAsync(T @event);
    }
}