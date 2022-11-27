using Convey.Persistence.MongoDB;
using FoodRocket.Services.Identity.Core.Entities;
using FoodRocket.Services.Identity.Core.Repositories;
using FoodRocket.Services.Identity.Infrastructure.Mongo.Documents;
using MongoDB.Bson;

namespace FoodRocket.Services.Identity.Infrastructure.Mongo.Repositories;

internal sealed  class UserRepository : IUserRepository
{
    private readonly IMongoRepository<UserDocument, long> _repository;

    public UserRepository(IMongoRepository<UserDocument, long> repository)
    {
        _repository = repository;
    }

    public async Task<User> GetAsync(long id)
    {
        var user = await _repository.GetAsync(id);

        return user?.AsEntity();
    }

    public async Task<User> GetAsync(string email)
    {
        var user = await _repository.GetAsync(x => x.Email == email.ToLowerInvariant());

        return user?.AsEntity();
    }

    public Task AddAsync(User user) => _repository.AddAsync(user.AsDocument());
}
