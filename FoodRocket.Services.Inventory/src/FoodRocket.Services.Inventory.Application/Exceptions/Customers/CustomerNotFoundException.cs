using FoodRocket.Services.Inventory.Application.Exceptions;

namespace FoodRocket.Services.Inventory.Application.Exceptions.Customers;

public class CustomerNotFoundException : AppException
{
    public override string Code { get; } = "customer_not_found";
    public long Id { get; }

    public CustomerNotFoundException(long id) : base($"Customer (id:{id}) is not found.")
        => (Id) = (id);
}
