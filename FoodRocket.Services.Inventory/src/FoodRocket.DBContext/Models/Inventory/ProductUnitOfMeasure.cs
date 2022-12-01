using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Inventory;

public class ProductUnitOfMeasure
{
    public long ProductId { get; set; }

    public long UnitOfMeasureId { get; set; }

    public Product Product { get; set; }

    public UnitOfMeasure UnitOfMeasure { get; set; }
}