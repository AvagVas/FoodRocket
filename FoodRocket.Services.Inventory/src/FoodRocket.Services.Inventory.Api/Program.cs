using System.Reflection;
using System.Text;
using FoodRocket.Services.Inventory.Application;
using Convey;
using Convey.Auth;
using Convey.Logging;
using Convey.Secrets.Vault;
using Convey.Types;
using Convey.WebApi;
using FoodRocket.Services.Inventory.Api.Routes;
using Microsoft.AspNetCore;
using FoodRocket.Services.Inventory.Application.Commands;
using FoodRocket.Services.Inventory.Application.Queries;
using FoodRocket.Services.Inventory.Application.Services;
using FoodRocket.Services.Inventory.Infrastructure;
using Microsoft.OpenApi.Models;

public class Program
{
    /// <summary>
    /// Entry main (just for demo, to have xml generated, I had exception.
    /// </summary>
    /// <param name="args"></param>
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
                            Title = "FoodRocket Inventory",
                            Description = "FoodRocket Inventory.",
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

// docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=d0_n0t_be_l@zy_h3r3" --name=sqlserver -e "MSSQL_PID=Express" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
// docker network connect simple-network sqlserver
// docker network inspect simple-network