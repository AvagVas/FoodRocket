using System;

namespace FoodRocket.Services.Inventory.Application.Exceptions
{
    public class StorageNotFoundException : AppException
    {
        public override string Code { get; } = "storage_of_not_found";
        public long Id { get; }

        public StorageNotFoundException(long id) : base($"Storage (id:{id}) is not found.")
            => (Id) = (id);
    }
}