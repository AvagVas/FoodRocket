using System.Reflection;
using System.Text;
using FoodRocket.Services.Identity.Application;
using Convey;
using Convey.Auth;
using Convey.Logging;
using Convey.Secrets.Vault;
using Convey.Types;
using Convey.WebApi;
using FoodRocket.Services.Identity.Api.Routes;
using Microsoft.AspNetCore;
using FoodRocket.Services.Identity.Application.Commands;
using FoodRocket.Services.Identity.Application.Queries;
using FoodRocket.Services.Identity.Application.Services;
using FoodRocket.Services.Identity.Infrastructure;
using Microsoft.OpenApi.Models;

public class Program
{
    public static async Task Main(string[] args)
        => await WebHost.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen(option =>
                    {
                        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Description =
                                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey
                        });
                        option.SwaggerDoc("v1", new OpenApiInfo
                        {
                            Version = "v1",
                            Title = "FoodRocket Identity",
                            Description = "FoodRocket Identity microservice for system security and user management.",
                            TermsOfService = new Uri("https://foodrocket.com/terms"),
                            Contact = new OpenApiContact
                            {
                                Name = "Example Contact",
                                Url = new Uri("https://foodrocket.com/contact")
                            },
                            License = new OpenApiLicense
                            {
                                Name = "Example License",
                                Url = new Uri("https://foodrocket.com/license")
                            }
                        });
                        option.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] { }
                            }
                        });

                        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                        option.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, xmlFilename));
                    })
                    .AddConvey()
                    .AddWebApi()
                    .AddApplication()
                    .AddInfrastructure()
                    .Build();
            })
            .Configure(app =>
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseInfrastructure();
                app.UseAllRoutes();
            })
            .UseLogging()
            .UseVault()
            .Build()
            .RunAsync();
}