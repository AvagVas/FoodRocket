using Convey.CQRS.Commands;

namespace FoodRocket.Services.Inventory.Application.Commands.Inventory;

public class DeleteProduct : ICommand
{
    public long ProductId { get; set; } = 0;
}