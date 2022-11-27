using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace FoodRocket.Services.Inventory.Application.Events.Customers.External;

[Message("identity", routingKey:"signed_up")]
public class SignedUp : IEvent
{
    public long UserId { get; set; }
    public string? Email { get; set;  }

    public string? UserType { get; set; }
    
    public string? Role { get; set;  }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

}