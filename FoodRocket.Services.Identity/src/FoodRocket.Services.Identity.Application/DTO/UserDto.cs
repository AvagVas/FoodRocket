using FoodRocket.Services.Identity.Core.Entities;

namespace FoodRocket.Services.Identity.Application.DTO;

public class UserDto
{
    public string Id { get; set; }
    public int OrganizationId { get; set; }
    public string Email { get; set; }

    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    public string UserType { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<string> Permissions { get; set; }

    public UserDto()
    {
    }

    public UserDto(User user)
    {
        Id = user.Id.ToString();
        OrganizationId = user.OrganizationId;
        Email = user.Email;
        Role = user.Role;
        CreatedAt = user.CreatedAt;
        Permissions = user.Permissions;
    }
}
