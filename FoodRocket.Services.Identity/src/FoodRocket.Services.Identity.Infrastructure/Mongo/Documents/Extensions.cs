using FoodRocket.Services.Identity.Infrastructure.Mongo.Documents;
using FoodRocket.Services.Identity.Application.DTO;
using FoodRocket.Services.Identity.Core.Entities;
using Thrift.Protocol;

namespace FoodRocket.Services.Identity.Infrastructure.Mongo.Documents;

internal static class Extensions
{
    public static User AsEntity(this UserDocument document)
    {
        UserType userType = Enum.Parse<UserType>(document.UserType);
        return new User(document.Id, document.OrganizationId, document.Email, document.Password, userType,
            document.Role, document.CreatedAt, document.FirstName, document.LastName,
            document.Permissions);
    }

    public static UserDocument AsDocument(this User entity)
        => new UserDocument
        {
            Id = entity.Id,
            OrganizationId = entity.OrganizationId,
            Email = entity.Email,
            Password = entity.Password,
            UserType = entity.UserType.ToString(),
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Role = entity.Role,
            CreatedAt = entity.CreatedAt,
            Permissions = entity.Permissions ?? Enumerable.Empty<string>()
        };

    public static UserDto AsDto(this UserDocument document)
        => new UserDto
        {
            Id = document.Id.ToString(),
            OrganizationId = document.OrganizationId,
            Email = document.Email,
            UserType = document.UserType,
            FirstName = document.FirstName,
            LastName = document.LastName,
            Role = document.Role,
            CreatedAt = document.CreatedAt,
            Permissions = document.Permissions ?? Enumerable.Empty<string>()
        };

    public static RefreshToken AsEntity(this RefreshTokenDocument document)
        => new RefreshToken(document.Id, document.UserId, document.Token, document.CreatedAt, document.RevokedAt);
        
    public static RefreshTokenDocument AsDocument(this RefreshToken entity)
        => new RefreshTokenDocument
        {
            Id = entity.Id,
            UserId = entity.UserId,
            Token = entity.Token,
            CreatedAt = entity.CreatedAt,
            RevokedAt = entity.RevokedAt
        };
}
