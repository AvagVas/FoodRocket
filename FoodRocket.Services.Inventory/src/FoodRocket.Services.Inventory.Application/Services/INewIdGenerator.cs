namespace FoodRocket.Services.Inventory.Application.Services;

public interface INewIdGenerator
{
    long GetNewIdFor(string generatorName);
}