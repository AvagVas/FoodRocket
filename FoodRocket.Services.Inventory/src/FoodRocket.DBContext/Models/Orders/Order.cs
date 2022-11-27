using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodRocket.DBContext.Models.Orders;

public class Order
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long OrderId { get; set; }

    public DateTime CreateOn { get; set; }

    public OrderStatus Status { get; set; }
    public ICollection<OrderItem> Items { get; set; }

    public long CustomerId { get; set; }

    public long ManagerId { get; set; }

    public long WaiterId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalSum { get; set; }
}