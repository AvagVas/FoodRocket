using FoodRocket.DBContext.Contexts;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodRocket.Services.Inventory.Infrastructure.SqlServer.Inventory.Repositories;

public class StorageRepository : IStorageRepository
{
    private readonly InventoryDbContext _inventoryContexts;

    public StorageRepository(InventoryDbContext inventoryContexts)
    {
        _inventoryContexts = inventoryContexts;
    }
    public Task<Storage?> GetAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(long id)
    {
        var test = await _inventoryContexts.Products.FirstOrDefaultAsync();
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(string storageName)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Storage product)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Storage product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }
}