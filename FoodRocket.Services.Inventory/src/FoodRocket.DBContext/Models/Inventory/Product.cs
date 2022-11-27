using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Inventory;

public class Product
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long ProductId { get; set; }
    
    [Required]
    [MaxLength(40)]
    public string Name { get; set; }
    
    public UnitOfMeasure MainUnitOfMeasure { get; set; }

    public ICollection<UnitOfMeasure> UnitOfMeasures;
}