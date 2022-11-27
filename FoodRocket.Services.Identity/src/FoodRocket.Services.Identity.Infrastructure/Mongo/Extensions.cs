using Convey.Persistence.MongoDB;
using FoodRocket.Services.Identity.Infrastructure.Mongo.Documents;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace FoodRocket.Services.Identity.Infrastructure.Mongo;

public static class Extensions
{
    public static IApplicationBuilder UseMongo(this IApplicationBuilder builder)
    {
        using (var scope = builder.ApplicationServices.CreateScope())
        {
            var users = scope.ServiceProvider.GetService<IMongoRepository<UserDocument, long>>().Collection;
            var userBuilder = Builders<UserDocument>.IndexKeys;
            Task.Run(async () => await users.Indexes.CreateOneAsync(
                new CreateIndexModel<UserDocument>(userBuilder.Ascending(i => i.Email),
                    new CreateIndexOptions
                    {
                        Unique = true
                    })));
        }

        return builder;
    }
}
