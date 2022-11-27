using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Orders;

public class Menu
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long MenuId { get; set; }

    public int Version { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public DateTime CreatedOn { get; set; }
    public long ChangedBy { get; set; }

    public ICollection<DishMenu> DishesLink { get; set; }
}