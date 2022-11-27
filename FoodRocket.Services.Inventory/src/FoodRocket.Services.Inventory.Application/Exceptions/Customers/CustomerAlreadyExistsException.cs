using FoodRocket.Services.Inventory.Application.Exceptions;

namespace FoodRocket.Services.Inventory.Application.Exceptions.Customers;

public class CustomerAlreadyExistsException : AppException
{
    public override string Code { get; } = "customer_already_exists";
    public long Id { get; }

    public CustomerAlreadyExistsException(long id) : base($"Customer (id:{id}) already exists.")
        => (Id) = (id);
}
