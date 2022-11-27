using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Staff;

public class Employee
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long EmployeeId { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [MaxLength(11)]
    public string SocialSecurityNumber { get; set; }

    [MaxLength(200)]
    public string Address { get; set; }

    [MaxLength(50)]
    public string Phone { get; set; }

    public bool IsDeleted { get; set; }
}