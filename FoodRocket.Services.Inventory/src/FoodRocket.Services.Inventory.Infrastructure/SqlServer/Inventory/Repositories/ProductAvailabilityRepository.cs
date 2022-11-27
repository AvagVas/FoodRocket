using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;

namespace FoodRocket.Services.Inventory.Infrastructure.SqlServer.Inventory.Repositories;

public class ProductAvailabilityRepository : IProductAvailabilityRepository
{
    public Task<ProductAvailability?> GetAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductAvailability?> GetAsync(long productId, long storageId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductAvailability>> GetRemaindersForAllStoragesAsync(long productId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(ProductAvailability productAvailability)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ProductAvailability productAvailability)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }
}