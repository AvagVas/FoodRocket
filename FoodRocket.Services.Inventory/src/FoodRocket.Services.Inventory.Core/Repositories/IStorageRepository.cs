using FoodRocket.Services.Inventory.Core.Entities.Inventory;

namespace FoodRocket.Services.Inventory.Core.Repositories;

public interface IStorageRepository
{
    Task<Storage?> GetAsync(long id);
    Task<bool> ExistsAsync(long id);
    Task<bool> ExistsAsync(string storageName);
    Task AddAsync(Storage storage);
    Task UpdateAsync(Storage storage);
    Task DeleteAsync(long id);
}