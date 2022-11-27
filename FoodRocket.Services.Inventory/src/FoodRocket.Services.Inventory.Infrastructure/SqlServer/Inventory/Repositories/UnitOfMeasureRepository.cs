using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;

namespace FoodRocket.Services.Inventory.Infrastructure.SqlServer.Inventory.Repositories;

public class UnitOfMeasureRepository : IUnitOfMeasureRepository
{
    public Task<UnitOfMeasure?> GetAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UnitOfMeasure>> GetListByIdsAsync(IEnumerable<long> ids)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByNameOrIdAsync(string name, long id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(IEnumerable<long> id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(UnitOfMeasure unitOfMeasure)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }
}