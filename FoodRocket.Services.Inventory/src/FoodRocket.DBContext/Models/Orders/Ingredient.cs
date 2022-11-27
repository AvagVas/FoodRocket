using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Orders;

public class Ingredient
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long IngredientId { get; set; }

    [Required]
    public long ProductId { get; set; }

    [Required]
    [MaxLength(40)]
    public string ProductName { get; set; }

    public ICollection<IngredientDish> DishesLink { get; set; }
}