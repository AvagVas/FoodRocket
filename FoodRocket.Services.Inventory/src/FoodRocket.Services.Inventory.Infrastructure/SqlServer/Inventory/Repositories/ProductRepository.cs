using FoodRocket.Services.Inventory.Core.Entities;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;
using FoodRocket.Services.Inventory.Core.ValueObjects;

namespace FoodRocket.Services.Inventory.Infrastructure.SqlServer.Inventory.Repositories;

public class ProductRepository : IProductRepository
{
    public Task<Product?> GetAsync(AggregateId id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(AggregateId id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(ProductName name)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(AggregateId id)
    {
        throw new NotImplementedException();
    }
}