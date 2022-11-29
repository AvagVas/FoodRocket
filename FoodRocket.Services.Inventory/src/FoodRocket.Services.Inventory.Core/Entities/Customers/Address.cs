using FoodRocket.Services.Inventory.Core.Exceptions.Customers;

namespace FoodRocket.Services.Inventory.Core.Entities.Customers;

public class Address
{
    public long Id { get; set; }

    public string? Country { get; private set; }

    public string? State { get; private set; }

    public string? City { get; private set; }

    public string? AddressLine { get; private set; }

    public string? ZipCode { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsDeleted { get; private set; }

    public Address(long id, string addressLine, string? country, string? state, string? city, string? zipCode,
        bool isActive, bool isDeleted = false)
    {
        ValidateAddress(id);
        Id = id;
        AddressLine = addressLine;
        Country = country;
        State = state;
        City = city;
        ZipCode = zipCode;
        IsActive = isActive;
        IsDeleted = isDeleted;
    }

    private void ValidateAddress(long id)
    {
        if (id <= 0)
        {
            throw new InvalidAddressException("Valid Id should be provided");
        }

        // TODO: Add some interesting domain related validations, when some time be available
    }
}