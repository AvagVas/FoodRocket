using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Orders;

public class DishMenu
{
    public long DishId { get; set; }
    public long MenuId { get; set; }
    public int Version { get; set; }
    public int Order { get; set; }

    public Menu Menu { get; set; }
    public Dish Dish { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Price { get; set; }
    
    public PriceOffer Promotion { get; set; }

    public DateTime PublishedOn { get; set; }
}