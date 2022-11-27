using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Inventory;

public class StorageProduct
{
    public long StorageId { get; set; }
    public long ProductId { get; set; }
    
    
    public Product Product { get; set; }

    public Storage Storage { get; set; }

    public UnitOfMeasure UnitOfMeasure { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Quantity { get; set; }
}