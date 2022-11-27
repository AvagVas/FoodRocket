namespace FoodRocket.Services.Inventory.Application.DTO.Inventory;

public class ProductDetailsDTO : ProductDTO
{
    public IEnumerable<long> AvailableUnitOfMeasureIds { get; set; } = Enumerable.Empty<long>();
    public IEnumerable<ProductAvailabilityDTO> ProductInStorages { get; set; } = Enumerable.Empty<ProductAvailabilityDTO>();
}