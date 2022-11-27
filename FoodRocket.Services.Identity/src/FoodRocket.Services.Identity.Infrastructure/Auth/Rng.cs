using System.Security.Cryptography;
using FoodRocket.Services.Identity.Application.Services;

namespace FoodRocket.Services.Identity.Infrastructure.Auth;

internal sealed class Rng : IRng
{
    private static readonly string[] SpecialChars = new[] {"/", "\\", "=", "+", "?", ":", "&"};

    public string Generate(int length = 50, bool removeSpecialChars = true)
    {
        var bytes = RandomNumberGenerator.GetBytes(length);
        var result = Convert.ToBase64String(bytes);

        return removeSpecialChars
            ? SpecialChars.Aggregate(result, (current, chars) => current.Replace(chars, string.Empty))
            : result;
    }
}
