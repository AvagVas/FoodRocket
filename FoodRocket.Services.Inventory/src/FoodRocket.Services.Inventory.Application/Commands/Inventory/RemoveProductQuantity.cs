using Convey.CQRS.Commands;

namespace FoodRocket.Services.Inventory.Application.Commands.Inventory;

public class RemoveProductQuantity : ICommand
{
    public long ProductId { get; set; } = 0;
    public long StorageId { get; set; } = 0;
    public long UnitOfMeasureId { get; set; } = 0;
    public decimal Quantity { get; set; } = 0;
}