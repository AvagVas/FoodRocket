using System;
using System.Collections.Generic;
using System.Linq;
using FoodRocket.Services.Identity.Core.Exceptions;

namespace FoodRocket.Services.Identity.Core.Entities;

public class User : AggregateRoot
{
    public int OrganizationId { get; private set; }
    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }
    public UserType UserType { get; set; }
    public string Role { get; private set; }
    public string Password { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public IEnumerable<string> Permissions { get; private set; }

    public User(long id, int organizationId, string email, string password, UserType userType, string role,
        DateTime createdAt,
        string firstName, string lastName,
        IEnumerable<string> permissions = null)

    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new InvalidEmailException(email);
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidPasswordException();
        }

        if (!Entities.Role.IsValid(role))
        {
            throw new InvalidRoleException(role);
        }

        Id = id;
        OrganizationId = organizationId;
        Email = email.ToLowerInvariant();
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
        Role = role.ToLowerInvariant();
        CreatedAt = createdAt;
        Permissions = permissions ?? Enumerable.Empty<string>();
    }
}
