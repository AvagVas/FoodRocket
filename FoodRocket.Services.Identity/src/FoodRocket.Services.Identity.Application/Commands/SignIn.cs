using System.ComponentModel.DataAnnotations;
using Convey.CQRS.Commands;
using Convey.WebApi.Requests;

namespace FoodRocket.Services.Identity.Application.Commands;

[Contract]
public class SignIn : IRequest
{ 
    public string Email { get; set; }
    public string Password { get; set; }

    public SignIn(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
