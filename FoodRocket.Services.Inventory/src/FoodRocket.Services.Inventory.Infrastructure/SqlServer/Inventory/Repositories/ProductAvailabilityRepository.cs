using FoodRocket.DBContext.Contexts;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodRocket.Services.Inventory.Infrastructure.SqlServer.Inventory.Repositories;

public class ProductAvailabilityRepository : IProductAvailabilityRepository
{
    private readonly InventoryDbContext _dbContext;

    public ProductAvailabilityRepository(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductAvailability?> GetAsync(long productId, long storageId)
    {
        var productsInStorages = await _dbContext.ProductsInStorages
            .Include(ps => ps.Product)
            .ThenInclude(product => product.MainUnitOfMeasure)
            .Include(ss => ss.Product.UnitOfMeasuresLink)
            .Include(ps => ps.Storage)
            .Where(ps =>
                ps.ProductId == productId &&
                ps.StorageId == storageId).FirstOrDefaultAsync();

        var baseUnitsOfMeasureDb = await _dbContext.UnitOfMeasures.Where(uomDb => uomDb.IsBase).ToListAsync();
        return productsInStorages?.AsEntity(baseUnitsOfMeasureDb);
    }

    public async Task<bool> ExistsAsync(long productId, long storageId)
    {
        var productsInStorages = await _dbContext.ProductsInStorages
            .AnyAsync(ps =>
                ps.ProductId == productId &&
                ps.StorageId == storageId);

        return productsInStorages;
    }
    public async Task<IEnumerable<ProductAvailability>> GetRemaindersForAllStoragesAsync(long productId)
    {
        var productsInStorages = await _dbContext.ProductsInStorages
            .Include(ps => ps.Product)
            .Include(ps => ps.Storage)
            .Where(ps =>
                ps.ProductId == productId).ToListAsync();

        var baseUnitsOfMeasureDb = await _dbContext.UnitOfMeasures.Where(uomDb => uomDb.IsBase).ToListAsync();
        return productsInStorages?.AsEntity(baseUnitsOfMeasureDb) ?? Enumerable.Empty<ProductAvailability>();
    }

    public async Task AddAsync(ProductAvailability productAvailability)
    {
        var productInStorageDb = productAvailability.AsDbModel();
        _dbContext.ProductsInStorages.Add(productInStorageDb);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductAvailability productAvailability)
    {
        var productInStorageDb = productAvailability.AsDbModel();
        var originalProductInStorageDb = await _dbContext.ProductsInStorages.FirstOrDefaultAsync(ps =>
            ps.StorageId == productInStorageDb.StorageId && ps.ProductId == productInStorageDb.ProductId);
        
        _dbContext.Entry(originalProductInStorageDb!).CurrentValues.SetValues(productInStorageDb);
    }
}