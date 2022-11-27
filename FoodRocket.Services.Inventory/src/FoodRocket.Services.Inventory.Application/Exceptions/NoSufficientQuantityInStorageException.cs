using System;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;

namespace FoodRocket.Services.Inventory.Application.Exceptions
{
    public class NoSufficientQuantityInStorageException : AppException
    {
        public override string Code { get; } = "no_sufficient_quantity_in_storage";

        public Product Product { get; }
        public Storage Storage { get; }
        public decimal RequiredQuantity { get; }
        public UnitOfMeasure RequiredUnitOfMeasure { get; }

        public NoSufficientQuantityInStorageException(Product product, Storage storage, decimal quantity,
            UnitOfMeasure requiredUnitOfMeasure) : base(
            $"No sufficient quantity in Storage: ({storage.Name}. Required {quantity} in {requiredUnitOfMeasure.Name})")
        {
            Product = product;
            Storage = storage;
            RequiredQuantity = quantity;
            RequiredUnitOfMeasure = requiredUnitOfMeasure;
        }
    }
}