using FoodRocket.Services.Identity.Application;

namespace FoodRocket.Services.Identity.Infrastructure.Contexts;

public interface IAppContextFactory
{
    IAppContext Create();
}
