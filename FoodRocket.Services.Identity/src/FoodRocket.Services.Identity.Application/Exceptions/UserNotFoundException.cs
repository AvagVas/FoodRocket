namespace FoodRocket.Services.Identity.Application.Exceptions;

public class UserNotFoundException : AppException
{
    public override string Code { get; } = "user_not_found";
    public long UserId { get; }
        
    public UserNotFoundException(long userId) : base($"User with ID: '{userId}' was not found.")
    {
        UserId = userId;
    }
}
