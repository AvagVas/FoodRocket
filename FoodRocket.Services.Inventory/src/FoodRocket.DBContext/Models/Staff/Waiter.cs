using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Staff;

public class Waiter
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long WaiterId { get; set; }

    public DateTime StartedOn { get; set; }
    public DateTime? FinishedOn { get; set; }

    public Manager Manager { get; set; }

    public Employee Employee { get; set; }
}