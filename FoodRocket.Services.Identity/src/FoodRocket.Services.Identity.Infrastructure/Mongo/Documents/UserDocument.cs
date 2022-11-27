
using Convey.Types;


namespace FoodRocket.Services.Identity.Infrastructure.Mongo.Documents;

public sealed class UserDocument :  IIdentifiable<long>
{
    public long Id { get; set; }

    public int OrganizationId { get; set; }
    public string Email { get; set; }

    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    public string UserType { get; set; }
    public string Role { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<string> Permissions { get; set; }
}
