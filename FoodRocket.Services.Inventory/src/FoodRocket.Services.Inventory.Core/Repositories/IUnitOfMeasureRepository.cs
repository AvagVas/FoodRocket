using FoodRocket.Services.Inventory.Core.Entities.Inventory;

namespace FoodRocket.Services.Inventory.Core.Repositories;

public interface IUnitOfMeasureRepository
{
    Task<UnitOfMeasure?> GetAsync(long id);
    Task<IEnumerable<UnitOfMeasure>> GetListByIdsAsync(IEnumerable<long> ids);

    Task<bool> ExistsByNameOrIdAsync(string name, long id);
    Task<bool> ExistsAsync(long id);
    Task<bool> ExistsAsync(IEnumerable<long> id);

    Task AddAsync(UnitOfMeasure unitOfMeasure);
    Task DeleteAsync(long id);
}