using Convey.CQRS.Queries;
using FoodRocket.Services.Inventory.Application.DTO.Inventory;

namespace FoodRocket.Services.Inventory.Application.Queries.Inventory;

public class GetProductDetailsListPaginated : IQuery<ProductDetailDTOsPaginatedList>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public long[] StorageIds { get; set; } = Array.Empty<long>();
}