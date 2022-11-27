using Convey.Persistence.MongoDB;
using FoodRocket.Services.Identity.Core.Entities;
using FoodRocket.Services.Identity.Core.Repositories;
using FoodRocket.Services.Identity.Infrastructure.Mongo.Documents;

namespace FoodRocket.Services.Identity.Infrastructure.Mongo.Repositories;

internal sealed class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly IMongoRepository<RefreshTokenDocument, long> _repository;

    public RefreshTokenRepository(IMongoRepository<RefreshTokenDocument, long> repository)
    {
        _repository = repository;
    }

    public async Task<RefreshToken> GetAsync(string token)
    {
        var refreshToken = await _repository.GetAsync(x => x.Token == token);

        return refreshToken?.AsEntity();
    }

    public Task AddAsync(RefreshToken token) => _repository.AddAsync(token.AsDocument());

    public Task UpdateAsync(RefreshToken token) => _repository.UpdateAsync(token.AsDocument());
}
