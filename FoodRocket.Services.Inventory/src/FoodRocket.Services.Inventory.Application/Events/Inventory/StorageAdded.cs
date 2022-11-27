using Convey.CQRS.Events;
using FoodRocket.Services.Inventory.Application.DTO.Inventory;

namespace FoodRocket.Services.Inventory.Application.Events.Inventory;

[Contract]
public class StorageAdded : IEvent
{
    public long StorageId { get; }

    public StorageAdded(long storageId)
    {
        StorageId = storageId;
    }
}