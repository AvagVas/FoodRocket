using System.Threading.Tasks;
using FoodRocket.Services.Identity.Core.Entities;

namespace FoodRocket.Services.Identity.Core.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}
