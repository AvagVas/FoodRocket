using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Orders;

public class IngredientDish
{
    public long IngredientId { get; set; }
    public long DishId { get; set; }
    
    public Ingredient Ingredient { get; set; }
    public Dish Dish { get; set; }

    [Required]
    public long RequiredInUnitOfMeasureId { get; set; }

    [Required]
    [MaxLength(20)]
    public string NameOfUnitOfMeasureId { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public long RequiredQuantity { get; set; }
}