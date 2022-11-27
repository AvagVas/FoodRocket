using Convey.Types;

namespace FoodRocket.Services.Identity.Infrastructure.Mongo.Documents;

internal sealed  class RefreshTokenDocument : IIdentifiable<long>
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Token { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? RevokedAt { get; set; }
}
