using System;

namespace FoodRocket.Services.Inventory.Application.Exceptions
{
    public class ProductNotFoundException : AppException
    {
        public override string Code { get; } = "product_not_found";
        public long Id { get; }

        public ProductNotFoundException(long id) : base($"Product (id:{id}) is not found.")
            => (Id) = (id);
    }
}