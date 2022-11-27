using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FoodRocket.DBContext.Models.Inventory;

public class UnitOfMeasure
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long UnitOfMeasureId { get; set; }

    public TypeOfUnitOfMeasure Type { get; set; }
    [MaxLength(20)]
    public string Name { get; set; }
    
    public bool IsBase { get; set; }
    public int Ratio { get; set; }

    public bool IsFractional { get; set; }
    
    [AllowNull]
    public long? BaseOfUnitOfMId { get; set; }
}