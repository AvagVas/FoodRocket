using Convey.CQRS.Queries;
using FoodRocket.Services.Inventory.Application.DTO.Inventory;

namespace FoodRocket.Services.Inventory.Application.Queries.Inventory;

public class GetUnitsOfMeasure : IQuery<IEnumerable<UnitOfMeasureDTO>>
{
}