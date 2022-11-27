using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Customers;

public class Contact
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long ContactId { get; set; }

    [Required] [MaxLength(100)]
    public string Name { get; set; }

    public ContactType ContactType { get; set; }

    [Required]
    [MaxLength(100)]
    public string Value { get; set; }

    public bool IsPrimary { get; set; }

    public Customer Customer { get; set; }
}