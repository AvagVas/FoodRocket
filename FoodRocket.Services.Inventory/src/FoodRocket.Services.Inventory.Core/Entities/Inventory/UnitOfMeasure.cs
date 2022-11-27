using FoodRocket.Services.Inventory.Core.Exceptions;
using FoodRocket.Services.Inventory.Core.ValueObjects;

namespace FoodRocket.Services.Inventory.Core.Entities.Inventory;

public class UnitOfMeasure
{
    public long Id { get; }
    public TypeOfUnitOfMeasure TypeOfUnitOfMeasure { get; }
    
    public string Name { get; }
    
    public bool IsBase { get; }

    public int Ratio { get; }
    
    public bool IsFractional { get; }
    public UnitOfMeasure? BaseOfUnitOfM { get; }



    public UnitOfMeasure(long id, TypeOfUnitOfMeasure type, string name, bool isBase, UnitOfMeasure? baseOfUnitOfM, int ratio, bool isFractional)
    {
        Validate(type, name, isBase, baseOfUnitOfM, ratio, isFractional);
        Id = id;
        TypeOfUnitOfMeasure = type;
        Name = name;
        IsBase = isBase;
        BaseOfUnitOfM = baseOfUnitOfM;
        Ratio = ratio;
        IsFractional = isFractional;
    }
    private static void Validate(TypeOfUnitOfMeasure type, string name, bool isBase, UnitOfMeasure? baseOfUnitOfM, int ratio, bool isFractional)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidUnitOfMeasureException();
        }

        if (!isBase && baseOfUnitOfM is null)
        {
            throw new InvalidUnitOfMeasureException();
        }

        if (isBase && ratio != 1)
        {
            throw new InvalidUnitOfMeasureException();
        }

        if (!isBase && baseOfUnitOfM is null)
        {
            throw new InvalidUnitOfMeasureException();
        }

        if (!isBase && baseOfUnitOfM is { IsBase: false })
        {
            throw new InvalidUnitOfMeasureException();
        }

        if (!isBase && baseOfUnitOfM is {} && baseOfUnitOfM.TypeOfUnitOfMeasure != type)
        {
            throw new InvalidUnitOfMeasureException();
        }
    }
    
    public override int GetHashCode()
        => Name.GetHashCode();

    public bool Equals(UnitOfMeasure? other) => other is {} && Name.Equals(other.Name);

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        return obj is UnitOfMeasure uom && Equals(uom);
    }
}