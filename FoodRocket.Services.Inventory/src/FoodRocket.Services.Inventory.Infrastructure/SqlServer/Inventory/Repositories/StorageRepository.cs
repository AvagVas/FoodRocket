using FoodRocket.DBContext.Contexts;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodRocket.Services.Inventory.Infrastructure.SqlServer.Inventory.Repositories;

public class StorageRepository : IStorageRepository
{
    private readonly InventoryDbContext _inventoryContexts;
    private readonly InventoryDbContext _dbContext;

    public StorageRepository(InventoryDbContext inventoryContexts, InventoryDbContext dbContext)
    {
        _inventoryContexts = inventoryContexts;
        _dbContext = dbContext;
    }
    public async Task<Storage?> GetAsync(long id)
    {
        var storageDb = await _dbContext.Storages.FirstOrDefaultAsync(s => s.StorageId == id);
        return storageDb?.AsEntity();
    }

    public async Task<bool> ExistsAsync(long id)
    {
        return await _dbContext.Storages.AnyAsync(s => s.StorageId == id);
    }

    public async Task<bool> ExistsAsync(string storageName)
    {
        return await _dbContext.Storages.AnyAsync(s => s.Name == storageName);
    }

    public async Task AddAsync(Storage storage)
    {
        DBContext.Models.Inventory.Storage storageDb = storage.AsDbModel();
        _dbContext.Storages.Add(storageDb);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Storage storage)
    {
        DBContext.Models.Inventory.Storage storageDb = storage.AsDbModel();
        _dbContext.Storages.Update(storageDb);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        DBContext.Models.Inventory.Storage storageDb = new DBContext.Models.Inventory.Storage() { StorageId = id };
        await _dbContext.SaveChangesAsync();
    }
}