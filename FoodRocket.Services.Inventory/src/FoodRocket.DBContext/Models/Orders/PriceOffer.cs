using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Orders;

public class PriceOffer
{
    public int PriceOfferId { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    [Required]
    public decimal NewPrice { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string PromotionalText { get; set; }
    
    public long DishId { get; set; }
}