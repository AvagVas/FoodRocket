using FoodRocket.Services.Inventory.Core.Entities;
using FoodRocket.Services.Inventory.Core.Entities.Customers;

namespace FoodRocket.Services.Inventory.Core.Repositories.Customers;

public interface ICustomerRepository
{
    Task<Customer?> GetAsync(AggregateId id);
    Task<bool> ExistsAsync(AggregateId id);
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(AggregateId id);
}