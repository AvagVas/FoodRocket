namespace FoodRocket.Services.Identity.Application.Services;

public interface INewIdGenerator
{
    long GetNewIdFor(string generatorName);
}
