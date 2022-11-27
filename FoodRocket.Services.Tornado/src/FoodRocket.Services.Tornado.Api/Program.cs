using Convey;
using Convey.Secrets.Vault;
using Convey.Logging;
using Convey.WebApi;
using FoodRocket.Services.Tornado.Api.Routes;
using Microsoft.AspNetCore;
using FoodRocket.Services.Tornado.Application;
using FoodRocket.Services.Tornado.Infrastructure;
using System.ComponentModel;

public class Program
{
    public static async Task Main(string[] args)
        => await CreateWebHostBuilder(args)
            .Build()
            .RunAsync();

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        // JsonSerializerOptions defaultOptions = new JsonSerializerOptions();
        // defaultOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        // defaultOptions.PropertyNameCaseInsensitive = true;
        // defaultOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
        // defaultOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        // defaultOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        // defaultOptions.Converters.Add(new BoolConverter());
        // defaultOptions.Converters.Add(new StringArrayConverter());

        //var jsonSerializer = new JsonSerializerFactory().GetSerializer();

        return WebHost.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services
                    .AddConvey()
                    .AddWebApi(/*conf => { }, jsonSerializer*/)
                    .AddApplication()
                    .AddInfrastructure()
                    .Build();
            })
            .Configure(app =>
            {
                //if (app.Environment.IsDevelopment())
                //{
                //    app.UseSwagger();
                //    app.UseSwaggerUI();
                //}
                //builder.Services.AddSwaggerGen();
                app.UseInfrastructure();
                app.UseAllRoutes();
                app.UseErrorHandler();
            })
            .UseLogging()
            .UseVault();
    }

}
