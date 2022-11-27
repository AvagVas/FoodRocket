namespace FoodRocket.Services.Inventory.Application.DTO.Inventory;

public class ProductAvailabilityDTO
{
    public long ProductId { get; set; } = 0;
    public long StorageId { get; set; } = 0;
    public string StorageName { get; set; } = "";
    public decimal Quantity { get; set; } = 0;
    public string NameOfUnitOfMeasure { get; set; } = "";
    public long UnitOfMeasureId { get; set; } = 0;
}