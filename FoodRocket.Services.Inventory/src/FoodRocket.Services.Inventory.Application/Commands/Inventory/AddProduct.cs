using System.Text.Json.Serialization;
using Convey.CQRS.Commands;

namespace FoodRocket.Services.Inventory.Application.Commands.Inventory;

public class AddProduct : ICommand
{
    [JsonIgnore]
    public long ProductId { get; set; } = 0;
    public string ProductName { get; set; } = "";
    public long MainUnitOfMeasureId { get; set; } = 0;
    public IEnumerable<long> UnitOfMeasures { get; } = Enumerable.Empty<long>();

    public bool AddInitialQuantity { get; set; } = false;
    public decimal InitialAmount { get; set; } = 0;
    public long? AmountInUnitOfMeasureId { get; set; } = 0;

    public long? StorageId { get; set; } = 0;
}