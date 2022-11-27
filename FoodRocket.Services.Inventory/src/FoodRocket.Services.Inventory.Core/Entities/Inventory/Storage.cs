using System.ComponentModel;
using FoodRocket.Services.Inventory.Core.Events;
using FoodRocket.Services.Inventory.Core.Exceptions;
using FoodRocket.Services.Inventory.Core.ValueObjects;

namespace FoodRocket.Services.Inventory.Core.Entities.Inventory;

public class Storage
{
    public long Id { get; }
    public string Name { get; private set; }

    public long ManagerId { get; private set; }

    public Storage(long storageId, string storageName, long managerId)
    {
        ValidateStorage(storageId, storageName, managerId);
        Id = storageId;
        Name = storageName;
        ManagerId = managerId;
    }

    public void ChangeName(string newName)
    {
        Name = newName;
    }
    
    public void ChangeManager(long newManagerId)
    {
        ManagerId = newManagerId;
    }
    private void ValidateStorage(long storageId, string storageName, long? managerId)
    {
        if (string.IsNullOrWhiteSpace(storageName))
        {
            throw new InvalidStorageException();
        }
    }
}