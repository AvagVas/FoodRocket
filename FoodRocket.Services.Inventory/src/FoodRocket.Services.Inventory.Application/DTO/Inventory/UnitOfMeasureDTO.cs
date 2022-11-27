namespace FoodRocket.Services.Inventory.Application.DTO.Inventory;

public class UnitOfMeasureDTO
{
    public long Id { get; set; } = 0;
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public bool IsBase { get; set; } = false;
    public int Ratio { get; set; } = 0;
    public bool IsFractional { get; set; } = false;
    public long BaseUnitOfMeasureId { get; set; } = 0;
}