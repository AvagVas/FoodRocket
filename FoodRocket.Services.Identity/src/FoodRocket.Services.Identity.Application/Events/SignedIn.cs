using Convey.CQRS.Events;

namespace FoodRocket.Services.Identity.Application.Events;

[Contract]
public class SignedIn : IEvent
{
    public long UserId { get; }
    public string Role { get; }

    public SignedIn(long userId, string role)
    {
        UserId = userId;
        Role = role;
    }
}
