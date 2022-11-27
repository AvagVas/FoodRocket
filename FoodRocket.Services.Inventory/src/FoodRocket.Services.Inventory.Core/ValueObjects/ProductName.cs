using System.Text.RegularExpressions;
using FoodRocket.Services.Inventory.Core.Exceptions;

namespace FoodRocket.Services.Inventory.Core.ValueObjects;

public struct ProductName : IComparable<ProductName>, IEquatable<ProductName>
{
    private readonly string _name;

    private ProductName(string name)
    {
        _name = name;
    }


    public static ProductName Create(string productName)
    {
        if (ValidateProductName(productName)) return new ProductName(productName);

        throw new InvalidProductNameException(productName);
    }

    public static bool ValidateProductName(string productName)
    {
        // TODO: should be discussed more complex validation of the product name if be needed
        if (string.IsNullOrWhiteSpace(productName))
        {
            return false;
        }

        return true;
    }

    public override string ToString()
    {
        return _name;
    }

    public int CompareTo(ProductName other)
    {
        return string.Compare(_name, other._name, StringComparison.InvariantCultureIgnoreCase);
    }

    public override int GetHashCode()
    {
        return _name.ToLower().GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is ProductName productName && Equals(productName);
    }
    public bool Equals(ProductName other)
    {
        return CompareTo(other) == 0;
    }

    public static bool operator ==(ProductName leftHandValue, ProductName rightHandValue)
    {
        return leftHandValue.Equals(rightHandValue);
    }

    public static bool operator !=(ProductName leftHandValue, ProductName rightHandValue)
    {
        return !leftHandValue.Equals(rightHandValue);
    }

    public static bool operator <(ProductName leftHandValue, ProductName rightHandValue)
    {
        return leftHandValue.CompareTo(rightHandValue) < 0;
    }

    public static bool operator >(ProductName leftHandValue, ProductName rightHandValue)
    {
        return leftHandValue.CompareTo(rightHandValue) > 0;
    }

    public static bool operator <=(ProductName leftHandValue, ProductName rightHandValue)
    {
        return leftHandValue.CompareTo(rightHandValue) <= 0;
    }

    public static bool operator >=(ProductName leftHandValue, ProductName rightHandValue)
    {
        return leftHandValue.CompareTo(rightHandValue) >= 0;
    }
    
    public static implicit operator string(ProductName productName)
    {
        return productName._name;
    }

    public static implicit operator ProductName(string name)
    {
        return new(name);
    }
}