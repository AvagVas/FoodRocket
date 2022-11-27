using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Customers;

public class Address
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long AddressId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Country { get; set; }
    
    [Required]
    [MaxLength(30)]
    public string State { get; set; }

    [Required]
    [MaxLength(30)]
    public string City { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string AddressLine { get; set; }

    [Required]
    [MaxLength(200)]
    public string ZipCode { get; set; }

    public bool IsActive { get; set; }
    
    public bool IsDeleted { get; set; }
    public Customer Customer { get; set; }
}