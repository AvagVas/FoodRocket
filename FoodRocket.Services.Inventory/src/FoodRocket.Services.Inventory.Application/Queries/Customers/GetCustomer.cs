using Convey.CQRS.Queries;
using FoodRocket.Services.Inventory.Application.DTO;
using FoodRocket.Services.Inventory.Application.DTO.Customers;

namespace FoodRocket.Services.Inventory.Application.Queries.Customers;

public class GetCustomer : IQuery<CustomerDTO>
{
    public string CustomerId { get; set; } = "";
}