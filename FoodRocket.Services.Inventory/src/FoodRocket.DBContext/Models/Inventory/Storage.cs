using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FoodRocket.DBContext.Models.Inventory;

public class Storage
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long StorageId { get; set; }
    
    [Required]
    [MaxLength(40)]
    public string Name { get; set; }

    [AllowNull]
    public long? ManagerId { get; set; }
}