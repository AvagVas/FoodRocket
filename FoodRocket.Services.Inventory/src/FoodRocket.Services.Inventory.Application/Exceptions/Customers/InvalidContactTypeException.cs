using FoodRocket.Services.Inventory.Application.Exceptions;

namespace FoodRocket.Services.Inventory.Application.Exceptions.Customers;

public class InvalidContactTypeException : AppException
{
    public override string Code { get; } = "invalid_contact_type";
    public string ContactType { get; }

    public InvalidContactTypeException(string contactType) : base($"Invalid contact type value: {contactType}.")
        => (ContactType) = (contactType);
}
