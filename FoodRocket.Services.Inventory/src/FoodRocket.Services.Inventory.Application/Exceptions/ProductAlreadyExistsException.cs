using System;

namespace FoodRocket.Services.Inventory.Application.Exceptions
{
    public class ProductAlreadyExistsException : AppException
    {
        public override string Code { get; } = "product_already_exists";
        public long Id { get; }

        public ProductAlreadyExistsException(long id) : base($"Product with id: {id}, already exists.")
            => (Id) = (id);
    }
}