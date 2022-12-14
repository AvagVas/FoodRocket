using System;
using FoodRocket.Services.Identity.Core.Exceptions;

namespace FoodRocket.Services.Identity.Core.Entities;


public class AggregateId : IEquatable<AggregateId>
{
    public long Value { get; }
    public AggregateId(long value)
    {
        if (value == 0)
        {
            throw new InvalidAggregateIdException();
        }

        Value = value;
    }

    public bool Equals(AggregateId other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || Value.Equals(other.Value);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((AggregateId) obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static implicit operator long(AggregateId id)
        => id.Value;

    public static implicit operator AggregateId(long id)
        => new AggregateId(id);

    public override string ToString() => Value.ToString();
}
