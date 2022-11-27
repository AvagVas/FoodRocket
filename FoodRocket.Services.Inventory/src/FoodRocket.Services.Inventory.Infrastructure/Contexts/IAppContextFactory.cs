using FoodRocket.Services.Inventory.Application;

namespace FoodRocket.Services.Inventory.Infrastructure.Contexts;

public interface IAppContextFactory
{
    IAppContext Create();
}
