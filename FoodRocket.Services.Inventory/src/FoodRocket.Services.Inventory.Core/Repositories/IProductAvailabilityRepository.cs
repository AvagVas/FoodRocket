using FoodRocket.Services.Inventory.Core.Entities.Inventory;

namespace FoodRocket.Services.Inventory.Core.Repositories;

public interface IProductAvailabilityRepository
{
    Task<ProductAvailability?> GetAsync(long productId, long storageId);
    Task<IEnumerable<ProductAvailability>> GetRemaindersForAllStoragesAsync(long productId);

    Task<bool> ExistsAsync(long productId, long storageId);
    Task AddAsync(ProductAvailability productAvailability);
    Task UpdateAsync(ProductAvailability productAvailability);
}