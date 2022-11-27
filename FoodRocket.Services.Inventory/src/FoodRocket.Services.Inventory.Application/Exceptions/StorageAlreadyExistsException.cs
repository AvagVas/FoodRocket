using System;

namespace FoodRocket.Services.Inventory.Application.Exceptions
{
    public class StorageAlreadyExistsException : AppException
    {
        public override string Code { get; } = "storage_already_exists";
        public long Id { get; }
        public string StorageName { get; }

        public StorageAlreadyExistsException(long id, string storageName) : base($"Storage with id: {id}, name: {storageName}, already exists.")
            => (Id, StorageName) = (id, storageName);
    }
}