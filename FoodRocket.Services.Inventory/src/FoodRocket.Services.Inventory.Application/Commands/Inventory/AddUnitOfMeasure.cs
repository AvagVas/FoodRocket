using Convey.CQRS.Commands;
using FoodRocket.Services.Inventory.Core.ValueObjects;

namespace FoodRocket.Services.Inventory.Application.Commands.Inventory;

public class AddUnitOfMeasure : ICommand
{
    public long UnitOfMeasureId { get; set; } = 0;
    public string TypeOfUnitOfMeasure { get; set; } = "";
    public string Name { get; set; } = "";
    public bool IsBase { get; set; } = false;
    public int Ratio { get; set; } = 0;
    public bool IsFractional { get; set; } = false;
    public long BaseUnitOfMeasureId { get; set; } = 0;
}