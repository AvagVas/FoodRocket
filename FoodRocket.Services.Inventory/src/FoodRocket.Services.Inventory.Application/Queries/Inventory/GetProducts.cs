using Convey.CQRS.Queries;
using FoodRocket.Services.Inventory.Application.DTO.Inventory;

namespace FoodRocket.Services.Inventory.Application.Queries.Inventory;

public class GetProducts : IQuery<IEnumerable<ProductDTO>>
{
    public long StorageId { get; set; }
}