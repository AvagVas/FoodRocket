using FoodRocket.DBContext.Services.SettingOptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FoodRocket.DBContext.Contexts;

public class OrdersDbContextFactory : IDesignTimeDbContextFactory<OrdersDbContext>
{
    public OrdersDbContext CreateDbContext(string[] args = null)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        //var iDGeneratorOptions = builder.GetOptions<IDGeneratorConfigurationOptions>(_IDGeneratosrSectionName);

        var optionsBuilder = new DbContextOptionsBuilder<OrdersDbContext>();
        optionsBuilder
            // Uncomment the following line if you want to print generated
            // SQL statements on the console.
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .UseSqlServer(configuration["SqlServer:ConnectionString"]);

        return new OrdersDbContext(optionsBuilder.Options);
    }
}