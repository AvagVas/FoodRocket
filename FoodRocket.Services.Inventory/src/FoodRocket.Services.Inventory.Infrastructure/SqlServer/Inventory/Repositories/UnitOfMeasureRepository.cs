using FoodRocket.DBContext.Contexts;
using FoodRocket.Services.Inventory.Core.Entities.Inventory;
using FoodRocket.Services.Inventory.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodRocket.Services.Inventory.Infrastructure.SqlServer.Inventory.Repositories;

public class UnitOfMeasureRepository : IUnitOfMeasureRepository
{
    private readonly InventoryDbContext _dbContext;

    public UnitOfMeasureRepository(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UnitOfMeasure?> GetAsync(long id)
    {
        var unitOfMeasureDb = await _dbContext.UnitOfMeasures.FirstOrDefaultAsync(uomDb => uomDb.UnitOfMeasureId == id);
        var baseUnitsOfMeasureDb = await _dbContext.UnitOfMeasures.Where(uomDb => uomDb.IsBase).ToListAsync();
        return unitOfMeasureDb?.AsEntity(baseUnitsOfMeasureDb);
    }

    public async Task<List<UnitOfMeasure>> GetListByIdsAsync(IEnumerable<long> ids)
    {
        var unitOfMeasureDbs = await _dbContext.UnitOfMeasures.Where(uomDb => ids.Contains(uomDb.UnitOfMeasureId))
            .ToListAsync();
        var baseUnitsOfMeasureDb = await _dbContext.UnitOfMeasures.Where(uomDb => uomDb.IsBase).ToListAsync();
        return unitOfMeasureDbs.AsEntity(baseUnitsOfMeasureDb);
    }

    public async Task<bool> ExistsByNameOrIdAsync(string name, long id)
    {
        return await _dbContext.UnitOfMeasures.AnyAsync(uomDb => uomDb.Name == name  || uomDb.UnitOfMeasureId == id);
    }

    public async Task<bool> ExistsAsync(long id)
    {
        return await _dbContext.UnitOfMeasures.AnyAsync(uomDb => uomDb.UnitOfMeasureId == id);
    }

    public async Task<bool> ExistsAsync(IEnumerable<long> ids)
    {
        return await _dbContext.UnitOfMeasures.AnyAsync(uomDb => ids.Contains(uomDb.UnitOfMeasureId));
    }

    public async Task AddAsync(UnitOfMeasure unitOfMeasure)
    {
        var uomDb = unitOfMeasure.AsDbModel();

        var baseUomDb = unitOfMeasure.BaseOfUnitOfM?.AsDbModel();
        if (baseUomDb is {} && !await _dbContext.UnitOfMeasures.AnyAsync(uom => uom.UnitOfMeasureId == baseUomDb.UnitOfMeasureId))
        {
            _dbContext.UnitOfMeasures.Add(baseUomDb);
        }

        _dbContext.UnitOfMeasures.Add(uomDb);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var uomDbToBeDeleted = new DBContext.Models.Inventory.UnitOfMeasure() { UnitOfMeasureId = id };
        _dbContext.Remove(uomDbToBeDeleted);
        await _dbContext.SaveChangesAsync();
    }
}