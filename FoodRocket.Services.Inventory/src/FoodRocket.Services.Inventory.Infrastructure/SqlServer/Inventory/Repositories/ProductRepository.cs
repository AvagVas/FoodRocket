using FoodRocket.DBContext.Contexts;
using FoodRocket.DBContext.Models.Inventory;
using FoodRocket.Services.Inventory.Core.Entities;
using FoodRocket.Services.Inventory.Core.Repositories;
using FoodRocket.Services.Inventory.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Product = FoodRocket.Services.Inventory.Core.Entities.Inventory.Product;
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
        var productDb = await _dbContext.Products
            .Include(product => product.MainUnitOfMeasure)
            .Include(product => product.UnitOfMeasuresLink)
            .ThenInclude(pUOfM => pUOfM.UnitOfMeasure)
            .FirstOrDefaultAsync(p => p.ProductId == id);
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

        List<long> neededIdsToFetch = new();
        neededIdsToFetch.AddRange(productDb.UnitOfMeasuresLink.Select(uom => uom.UnitOfMeasureId));
        neededIdsToFetch.Add(productDb.MainUnitOfMeasure.UnitOfMeasureId);
        var originalUoMs = await _dbContext.UnitOfMeasures.Where(uom =>
            neededIdsToFetch.Contains(uom.UnitOfMeasureId)).ToListAsync();

        var foundMainUnitOfMeasureOfProduct =
            originalUoMs.FirstOrDefault(uom => uom.UnitOfMeasureId == productDb.MainUnitOfMeasure.UnitOfMeasureId);

        if (foundMainUnitOfMeasureOfProduct is { })
        {
            productDb.MainUnitOfMeasure = foundMainUnitOfMeasureOfProduct;
        }
        else
        {
            _dbContext.UnitOfMeasures.Add(productDb.MainUnitOfMeasure);
        }

        foreach (var productUnitOfMeasure in productDb.UnitOfMeasuresLink.ToList())
        {
            var foundOriginalUoM =
                originalUoMs.FirstOrDefault(uom => uom.UnitOfMeasureId == productUnitOfMeasure.UnitOfMeasureId);

            if (productUnitOfMeasure.UnitOfMeasureId == productDb.MainUnitOfMeasure.UnitOfMeasureId)
            {
                productUnitOfMeasure.UnitOfMeasure = productDb.MainUnitOfMeasure;
                continue;
            }

            if (foundOriginalUoM is { })
            {
                productUnitOfMeasure.UnitOfMeasure = foundOriginalUoM;
            }
            else
            {
                _dbContext.UnitOfMeasures.Add(productUnitOfMeasure.UnitOfMeasure);
            }
        }

        _dbContext.Products.Add(productDb);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        var productDb = product.AsDbModel();
        var originalProductDb =
            await _dbContext.Products.FirstOrDefaultAsync(pr => pr.ProductId == productDb.ProductId);

        List<long> neededIdsToFetch = new();
        neededIdsToFetch.AddRange(productDb.UnitOfMeasuresLink.Select(uom => uom.UnitOfMeasureId));
        neededIdsToFetch.Add(productDb.MainUnitOfMeasure.UnitOfMeasureId);
        var originalUoMs = await _dbContext.UnitOfMeasures.Where(uom =>
            neededIdsToFetch.Contains(uom.UnitOfMeasureId)).ToListAsync();

        var foundMainUnitOfMeasureOfProduct =
            originalUoMs.FirstOrDefault(uom => uom.UnitOfMeasureId == productDb.MainUnitOfMeasure.UnitOfMeasureId);

        if (foundMainUnitOfMeasureOfProduct is { })
        {
            productDb.MainUnitOfMeasure = foundMainUnitOfMeasureOfProduct;
        }
        else
        {
            _dbContext.UnitOfMeasures.Add(productDb.MainUnitOfMeasure);
        }

        foreach (var productUnitOfMeasure in productDb.UnitOfMeasuresLink.ToList())
        {
            productUnitOfMeasure.Product = originalProductDb;
            var foundOriginalUoM =
                originalUoMs.FirstOrDefault(uom => uom.UnitOfMeasureId == productUnitOfMeasure.UnitOfMeasureId);

            if (productUnitOfMeasure.UnitOfMeasureId == productDb.MainUnitOfMeasure.UnitOfMeasureId)
            {
                productUnitOfMeasure.UnitOfMeasure = productDb.MainUnitOfMeasure;
                continue;
            }

            if (foundOriginalUoM is { })
            {
                productUnitOfMeasure.UnitOfMeasure = foundOriginalUoM;
            }
            else
            {
                _dbContext.UnitOfMeasures.Add(productUnitOfMeasure.UnitOfMeasure);
                _dbContext.ProductUnitOfMeasures.Add(productUnitOfMeasure);
            }
        }

        _dbContext.Entry(originalProductDb!).CurrentValues.SetValues(productDb);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(AggregateId id)
    {
        DBContext.Models.Inventory.Product productDb = new DBContext.Models.Inventory.Product() { ProductId = id };
        _dbContext.Remove(productDb);
        await _dbContext.SaveChangesAsync();
    }
}