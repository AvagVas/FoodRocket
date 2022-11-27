using FoodRocket.Services.Inventory.Core.Entities;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.ValueObjects;

namespace FoodRocket.Services.Inventory.Core.Repositories;

public interface IProductRepository
{
    Task<Product?> GetAsync(AggregateId id);
    Task<bool> ExistsAsync(AggregateId id);
    Task<bool> ExistsAsync(ProductName name);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(AggregateId id);
}