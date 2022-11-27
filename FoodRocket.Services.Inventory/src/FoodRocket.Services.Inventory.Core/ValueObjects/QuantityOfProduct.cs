using System.Text.RegularExpressions;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Exceptions;

namespace FoodRocket.Services.Inventory.Core.ValueObjects;

public class QuantityOfProduct : IComparable<QuantityOfProduct>, IEquatable<QuantityOfProduct>
{
    public Product Product { get; }
    public UnitOfMeasure UnitOfMeasure { get; }
    public decimal Quantity { get; }

    public QuantityOfProduct(Product product, UnitOfMeasure unitOfMeasure, decimal quantity)
    {
        ValidateQuantity(product, unitOfMeasure, quantity);
        Product = product;
        UnitOfMeasure = unitOfMeasure;
        Quantity = quantity;
    }

    public QuantityOfProduct ConvertTo(UnitOfMeasure targetUnitOfMeasure)
    {
        decimal newQantity = ConvertQuantity(Quantity, UnitOfMeasure, targetUnitOfMeasure);

        return new QuantityOfProduct(Product, targetUnitOfMeasure, newQantity);
    }
    
    public static decimal ConvertQuantity(decimal quantity, UnitOfMeasure left, UnitOfMeasure right)
    {
        if (left.TypeOfUnitOfMeasure == right.TypeOfUnitOfMeasure)
        {
            throw new UnitOfMeasuresMismatchException(left, right);
        }

        if (left.IsFractional == right.IsFractional && left.Ratio == right.Ratio)
        {
            return quantity;
        }
        
        decimal result;
        if (left.IsFractional)
        {
            result = quantity / left.Ratio;
        }
        else
        {
            result = quantity * left.Ratio;
        }

        if (right.IsFractional)
        {
            result *= right.Ratio;
        }
        else
        {
            result /= right.Ratio;
        }

        return result;
    }
    
    private decimal GetQuantityInMainUnitOfMeasure()
    {
        return ConvertQuantity(Quantity, UnitOfMeasure, Product.MainUnitOfMeasure);
    }
    
    private decimal GetQuantityInBaseUnitOfMeasure()
    {
        UnitOfMeasure? baseUnitOfMeasure = Product.MainUnitOfMeasure.BaseOfUnitOfM;
        if (baseUnitOfMeasure is null)
        {
            baseUnitOfMeasure = Product.MainUnitOfMeasure;
        }

        return ConvertQuantity(Quantity, UnitOfMeasure, baseUnitOfMeasure);
    }
    
    public static void ValidateQuantity(Product product, UnitOfMeasure unitOfMeasure, decimal quantity)
    {
        if (quantity < 0)
        {
            throw new InvalidQuantityOfProductException(product, unitOfMeasure, quantity);
        }

        if (product.MainUnitOfMeasure.TypeOfUnitOfMeasure != unitOfMeasure.TypeOfUnitOfMeasure)
        {
            throw new InvalidQuantityOfProductException(product, unitOfMeasure, quantity);
        }
    }

    public int CompareTo(QuantityOfProduct? other)
    {
        if (other is null) throw new ArgumentNullException(nameof(other));

        if (UnitOfMeasure.TypeOfUnitOfMeasure != other.UnitOfMeasure.TypeOfUnitOfMeasure)
        {
            throw new UnitOfMeasuresMismatchException(UnitOfMeasure, other.UnitOfMeasure);
        }

        return decimal.Compare(GetQuantityInMainUnitOfMeasure(), other.GetQuantityInMainUnitOfMeasure());
    }

    public bool Equals(QuantityOfProduct? other)
    {
        if (other is null) throw new ArgumentNullException(nameof(other));
        if (UnitOfMeasure.TypeOfUnitOfMeasure != other.UnitOfMeasure.TypeOfUnitOfMeasure)
        {
            throw new UnitOfMeasuresMismatchException(UnitOfMeasure, other.UnitOfMeasure);
        }

        return decimal.Compare(GetQuantityInMainUnitOfMeasure(), other.GetQuantityInMainUnitOfMeasure()) == 0;
    }
    

    public static QuantityOfProduct Create(Product product, UnitOfMeasure unitOfMeasure, decimal quantity)
    {
        return new QuantityOfProduct(product, unitOfMeasure, quantity);
    }
    
    
    public override string ToString()
    {
        return $"{Quantity:F4} {UnitOfMeasure.Name}";
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Product, UnitOfMeasure, Quantity);
    }
    
    public override bool Equals(object? obj)
    {
        return obj is QuantityOfProduct quantityOfProduct && Equals(quantityOfProduct);
    }

    public static bool operator ==(QuantityOfProduct leftHandValue, QuantityOfProduct rightHandValue)
    {
        return leftHandValue.Equals(rightHandValue);
    }
    
    public static bool operator !=(QuantityOfProduct leftHandValue, QuantityOfProduct rightHandValue)
    {
        return !leftHandValue.Equals(rightHandValue);
    }
    
    public static bool operator <(QuantityOfProduct leftHandValue, QuantityOfProduct rightHandValue)
    {
        return leftHandValue.CompareTo(rightHandValue) < 0;
    }
    
    public static bool operator >(QuantityOfProduct leftHandValue, QuantityOfProduct rightHandValue)
    {
        return leftHandValue.CompareTo(rightHandValue) > 0;
    }
    
    public static bool operator <=(QuantityOfProduct leftHandValue, QuantityOfProduct rightHandValue)
    {
        return leftHandValue.CompareTo(rightHandValue) <= 0;
    }
    
    public static bool operator >=(QuantityOfProduct leftHandValue, QuantityOfProduct rightHandValue)
    {
        return leftHandValue.CompareTo(rightHandValue) >= 0;
    }
    
    public static QuantityOfProduct operator +(QuantityOfProduct leftHandValue, QuantityOfProduct rightHandValue)
    {
        if (leftHandValue.Product.Id.Value != rightHandValue.Product.Id.Value)
        {
            throw new InvalidOperationOnQuantityOfProductException("Different products in operation.");
        }

        decimal quantity = leftHandValue.GetQuantityInMainUnitOfMeasure() + rightHandValue.GetQuantityInMainUnitOfMeasure();

        var quantityInMainUoM = new QuantityOfProduct(leftHandValue.Product, leftHandValue.Product.MainUnitOfMeasure, quantity);
        return quantityInMainUoM.ConvertTo(leftHandValue.UnitOfMeasure);
    }
    
    public static QuantityOfProduct operator -(QuantityOfProduct leftHandValue, QuantityOfProduct rightHandValue)
    {
        if (leftHandValue.Product.Id.Value != rightHandValue.Product.Id.Value)
        {
            throw new InvalidOperationOnQuantityOfProductException("Different products in operation.");
        }

        decimal quantity = leftHandValue.GetQuantityInMainUnitOfMeasure() - rightHandValue.GetQuantityInMainUnitOfMeasure();

        var quantityInMainUoM = new QuantityOfProduct(leftHandValue.Product, leftHandValue.Product.MainUnitOfMeasure, quantity);
        return quantityInMainUoM.ConvertTo(leftHandValue.UnitOfMeasure);
    }
}