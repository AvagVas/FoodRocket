using Convey.CQRS.Events;

namespace FoodRocket.Services.Identity.Application.Events;

[Contract]
public class SignedUp : IEvent
{
    public long UserId { get; }
    public string Email { get; }

    public string FirstName { get; }

    public string LastName { get; }
    public string UserType { get; }
    public string Role { get; }
        
    public SignedUp(long userId, string email, string userType,string role, string firstName, string lastName)
    {
        UserId = userId;
        Email = email;
        UserType = userType;
        Role = role;
        FirstName = firstName;
        LastName = lastName;
    }
}
