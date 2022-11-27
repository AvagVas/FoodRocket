namespace FoodRocket.Services.Inventory.Application.DTO.Inventory;

public class ProductDTO
{
    public long Id { get; set; } = 0;
    public string Name { get; set; } = "";
    public long MainUnitOfMeasureId { get; set; } = 0;
    public string MainUnitOfMeasureName { get; set; } = "";
}