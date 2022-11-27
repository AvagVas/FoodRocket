using Convey.CQRS.Commands;

namespace FoodRocket.Services.Inventory.Application.Commands.Inventory;

public class UpdateStorage : ICommand
{
    public long StorageId { get; set; } = 0;
    public string StorageName { get; set; } = "";
    public long ManagerId { get; set; } = 0;
}