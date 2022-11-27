namespace FoodRocket.DBContext.Services;

public interface INewIdGenerator
{
    long GetNewIdFor(string generatorName);
}