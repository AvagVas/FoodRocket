using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRocket.DBContext.Models.Customers;

public class Customer
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long CustomerId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public long PrimaryContactId { get; set; }

    public long MainShippingAddressId { get; set; }

    public long MainBillingAddressId { get; set; }

    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Address> Addresses { get; set; }

    public ICollection<Contact> Contacts { get; set; }

    public DateTime CreatedAt { get; set; }
}