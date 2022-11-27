using Convey.CQRS.Queries;
using FoodRocket.Services.Identity.Application.DTO;

namespace FoodRocket.Services.Identity.Application.Queries;

public class GetUser : IQuery<UserDto>
{
    public long UserId { get; set; }
}
