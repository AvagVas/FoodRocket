using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Staff;

public class Manager
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long ManagerId { get; set; }

    public ICollection<Waiter> Waiters { get; set; }
    public Employee Employee { get; set; }

    public DateTime StartedOn { get; set; }
    public DateTime? FinishedOn { get; set; }
}