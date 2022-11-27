using System;
using System.Threading.Tasks;
using FoodRocket.Services.Identity.Core.Entities;

namespace FoodRocket.Services.Identity.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(long id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
    }
}
