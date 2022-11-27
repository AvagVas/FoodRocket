using Convey.CQRS.Queries;
using FoodRocket.Services.Inventory.Application.DTO.Inventory;

namespace FoodRocket.Services.Inventory.Application.Queries.Inventory;

public class GetProductsListPaginated : IQuery<ProductDTOsPaginatedList>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public long[] StorageId { get; set; } = Array.Empty<long>();
}