using Convey.CQRS.Commands;

namespace FoodRocket.Services.Identity.Application.Commands;

[Contract]
public class SignUp : ICommand
{
    public int OrganizationId { get; }
    public string Email { get; }
    public string Password { get; }
    
    public string FirstName { get; }
    
    public string LastName { get; }
    public string UserType { get; set; }
    public string Role { get; }
    public IEnumerable<string> Permissions { get; }

    public SignUp(int organizationId, string email, string password, string userType,string role, IEnumerable<string> permissions, string firstName, string lastName)
    {
        OrganizationId = organizationId;
        Email = email;
        Password = password;
        UserType = userType;
        Role = role;
        FirstName = firstName;
        LastName = lastName;
        Permissions = permissions;
    }
}
