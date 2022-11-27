namespace FoodRocket.Services.Identity.Application.Exceptions;

public class InvalidUserTypeValueException : AppException
{
    public override string Code { get; } = "invalid_user_type_value";
    public string UserType { get; }
        
    public InvalidUserTypeValueException(string userType) : base($"$Invalid value for user type: {userType}.")
    {
        UserType = userType;
    }
}
