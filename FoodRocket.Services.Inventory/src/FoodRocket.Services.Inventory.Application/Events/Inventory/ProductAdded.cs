using Convey.CQRS.Events;
using FoodRocket.Services.Inventory.Application.DTO.Inventory;

namespace FoodRocket.Services.Inventory.Application.Events.Inventory;

[Contract]
public class ProductAdded : IEvent
{
    public long ProductId { get; }

    public ProductAdded(long productId)
    {
        ProductId = productId;
    }
}