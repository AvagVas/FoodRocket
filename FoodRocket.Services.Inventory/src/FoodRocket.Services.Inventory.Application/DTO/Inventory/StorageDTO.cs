namespace FoodRocket.Services.Inventory.Application.DTO.Inventory;

public class StorageDTO
{
    public long Id { get; set; } = 0;
    public string Name { get; set; } = "";
    public long ManagerId { get; set; } = 0;
    public string ManagerName { get; set; } = "";
}