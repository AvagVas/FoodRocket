using FoodRocket.DBContext.Contexts;
using FoodRocket.Services.Inventory.Core.Entities;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;
using FoodRocket.Services.Inventory.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Storage = FoodRocket.DBContext.Models.Inventory.Storage;

namespace FoodRocket.Services.Inventory.Infrastructure.SqlServer.Inventory.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly InventoryDbContext _dbContext;

    public ProductRepository(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetAsync(AggregateId id)
    {
        var productDb = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        var baseUnitsOfMeasureDb = await _dbContext.UnitOfMeasures.Where(uomDb => uomDb.IsBase).ToListAsync();
        return productDb?.AsEntity(baseUnitsOfMeasureDb);
    }

    public async Task<bool> ExistsAsync(AggregateId id)
    {
        return await _dbContext.Products.AnyAsync(p => p.ProductId == id);
    }

    public async Task<bool> ExistsAsync(ProductName name)
    {
        return await _dbContext.Products.AnyAsync(p => p.Name == name);
    }

    public async Task AddAsync(Product product)
    {
        var productDb = product.AsDbModel();
        _dbContext.Products.Add(productDb);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        var productDb = product.AsDbModel();
        _dbContext.Products.Update(productDb);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(AggregateId id)
    {
        DBContext.Models.Inventory.Product productDb = new DBContext.Models.Inventory.Product() { ProductId = id };
        _dbContext.Remove(productDb);
        await _dbContext.SaveChangesAsync();
    }
}