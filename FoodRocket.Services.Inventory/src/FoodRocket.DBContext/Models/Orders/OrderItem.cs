using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Orders;

public class OrderItem
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long OrderItemId { get; set; }
    
    public Order Order { get; set; }

    public DishMenu DishFromMenu { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    public decimal ItemTotalSum { get; set; }

    public PriceOffer AppliedPriceOffer { get; set; }
}