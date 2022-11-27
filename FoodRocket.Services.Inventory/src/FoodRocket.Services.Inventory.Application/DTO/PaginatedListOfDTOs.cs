namespace FoodRocket.Services.Inventory.Application.DTO;

public class PaginatedListOfDTOs<T>
{
    // 1 based
    public int PageNumber { get; set; } = 0;
    public bool IsLastPage { get; set; } = false;
    public bool IsFirstPage { get; set; } = false;
    public int Count { get; set; } = 0;
    public int PageSize { get; set; } = 0;

    private IEnumerable<T> Content { get; set; } = Enumerable.Empty<T>();
}