using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Orders;

public class Dish
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long DishId { get; set; }
    
    
    [Required]
    [MaxLength(150)]
    public string Name { get; set; }

    public string Description { get; set; }



    public ICollection<DishMenu> MenusLink { get; set; }

    public ICollection<IngredientDish> IngredientsLink { get; set; }
}