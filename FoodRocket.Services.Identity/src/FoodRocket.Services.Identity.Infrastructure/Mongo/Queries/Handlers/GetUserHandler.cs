using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using FoodRocket.Services.Identity.Application.DTO;
using FoodRocket.Services.Identity.Application.Queries;
using FoodRocket.Services.Identity.Infrastructure.Mongo.Documents;

namespace FoodRocket.Services.Identity.Infrastructure.Mongo.Queries.Handlers;

internal sealed  class GetUserHandler : IQueryHandler<GetUser, UserDto>
{
    private readonly IMongoRepository<UserDocument, long> _userRepository;

    public GetUserHandler(IMongoRepository<UserDocument, long> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> HandleAsync(GetUser query, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.GetAsync(query.UserId);

        return user?.AsDto();
    }
}
