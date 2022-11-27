using FoodRocket.Services.Inventory.Core.Exceptions.Customers;

namespace FoodRocket.Services.Inventory.Core.Entities.Customers;

public class Contact
{
    public long Id { get; }

    public string Name { get; }

    public ContactType ContactType { get; }

    public string Value { get; }

    public bool IsPrimary { get; private set; }

    public Contact(long id, ContactType contactType, string name, string value, bool isPrimary)
    {
        ValidateContact(id, contactType, name, value, isPrimary);
        Id = id;
        Name = name;
        ContactType = contactType;
        Value = value;
        IsPrimary = isPrimary;
    }

    public void ChangePrimaryStatus(bool status)
    {
        IsPrimary = status;
    }
    private void ValidateContact(long id, ContactType contactType, string name, string value, bool isPrimary)
    {
        if (id <= 0)
        {
            throw new InvalidContactException("Valid Id should be provided");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidContactException("Invalid name of contact");
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidContactException("Value of contact should be provided");
        }
    }
}