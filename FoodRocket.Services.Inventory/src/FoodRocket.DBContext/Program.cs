// See https://aka.ms/new-console-template for more information

using FoodRocket.DBContext;
using FoodRocket.DBContext.Contexts;
using FoodRocket.DBContext.Models;
using FoodRocket.DBContext.Services;
using FoodRocket.DBContext.Services.SettingOptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


using var inventoryDbContext = new InventoryDbContextFactory().CreateDbContext();


using var orderDbContext = new OrdersDbContextFactory().CreateDbContext();

using var customersDbContext = new CustomersDbContextFactory().CreateDbContext();

using var staffDbContext = new StaffDbContextFactory().CreateDbContext();

//    Inventory ->>>>>>    ./dotnet ef migrations add initial --project /home/vas/Projects/other/FoodRocket/FoodRocket.Services.Inventory/src/FoodRocket.DBContext/FoodRocket.DBContext.csproj --context FoodRocket.DBContext.Contexts.InventoryDbContext --configuration Debug --output-dir Migrations/Inventory
//    Orders ->>>>>>      ./dotnet ef migrations add initial --project /home/vas/Projects/other/FoodRocket/FoodRocket.Services.Inventory/src/FoodRocket.DBContext/FoodRocket.DBContext.csproj --context FoodRocket.DBContext.Contexts.OrdersDbContext --configuration Debug --output-dir Migrations/Orders
//    Customers ->>>>>>   ./dotnet ef migrations add initial --project /home/vas/Projects/other/FoodRocket/FoodRocket.Services.Inventory/src/FoodRocket.DBContext/FoodRocket.DBContext.csproj --context FoodRocket.DBContext.Contexts.CustomersDbContext --configuration Debug --output-dir Migrations/Customers
//    Staff ->>>>>>       ./dotnet ef migrations add initial --project /home/vas/Projects/other/FoodRocket/FoodRocket.Services.Inventory/src/FoodRocket.DBContext/FoodRocket.DBContext.csproj --context FoodRocket.DBContext.Contexts.StaffDbContext --configuration Debug --output-dir Migrations/Staff

//    Inventory ->>>>>>    ./dotnet ef database update --project /home/vas/Projects/other/FoodRocket/FoodRocket.Services.Inventory/src/FoodRocket.DBContext/FoodRocket.DBContext.csproj --context FoodRocket.DBContext.Contexts.InventoryDbContext
//    Orders ->>>>>>       ./dotnet ef database update --project /home/vas/Projects/other/FoodRocket/FoodRocket.Services.Inventory/src/FoodRocket.DBContext/FoodRocket.DBContext.csproj --context FoodRocket.DBContext.Contexts.OrdersDbContext
//    Customers ->>>>>>    ./dotnet ef database update --project /home/vas/Projects/other/FoodRocket/FoodRocket.Services.Inventory/src/FoodRocket.DBContext/FoodRocket.DBContext.csproj --context FoodRocket.DBContext.Contexts.CustomersDbContext
//    Staff ->>>>>>        ./dotnet ef database update --project /home/vas/Projects/other/FoodRocket/FoodRocket.Services.Inventory/src/FoodRocket.DBContext/FoodRocket.DBContext.csproj --context FoodRocket.DBContext.Contexts.StaffDbContext

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

IDGeneratorConfigurationOptions generatorOptions = new IDGeneratorConfigurationOptions();
configuration.Bind("IDGenerators", generatorOptions);
INewIdGenerator idGenerator = new NewIdGenerator(generatorOptions);

var bogus = new BogusGenerator(idGenerator);
var data = bogus.GetData();


await ReCreateDatabase(inventoryDbContext, orderDbContext, customersDbContext, staffDbContext, false);
//return;
await AddInventoryData(inventoryDbContext);
await AddOrdersData(orderDbContext);
await AddCustomersData(customersDbContext);
await AddStaffData(staffDbContext);

#region Adding data with relations

async Task ReCreateDatabase(InventoryDbContext context, OrdersDbContext ordersDb, CustomersDbContext customersDb,
    StaffDbContext staffDb, bool onlyDelete = false)
{
    await context.Database.EnsureDeletedAsync();
    Console.WriteLine("Database dropped");

    if (!onlyDelete)
    {
        await context.Database.MigrateAsync();
        await ordersDb.Database.MigrateAsync();
        await customersDb.Database.MigrateAsync();
        await staffDb.Database.MigrateAsync();
        Console.WriteLine("Database created");
        return;
    }
}

async Task AddInventoryData(InventoryDbContext context)
{
    Console.WriteLine("STARTED: AddInventoryData");
    await context.AddRangeAsync(data.Storages);
    await context.AddRangeAsync(data.Weights);
    await context.AddRangeAsync(data.Pieces);
    await context.AddRangeAsync(data.Volumes);
    await context.AddRangeAsync(data.Products);
    await context.AddRangeAsync(data.StorageProducts);
    await context.SaveChangesAsync();
    Console.WriteLine("FINISHED: AddInventoryData");
}

async Task AddOrdersData(OrdersDbContext context)
{
    Console.WriteLine("STARTED: AddOrdersData");
    await context.AddRangeAsync(data.Ingredients);
    await context.AddRangeAsync(data.Dishes);
    await context.AddRangeAsync(data.Promotions);
    await context.AddRangeAsync(data.Menus);
    await context.AddRangeAsync(data.DishesInMenu);
    await context.AddRangeAsync(data.DishesWithIngredients);
    await context.AddRangeAsync(data.Orders);
    //await context.AddRangeAsync(data.OrderItems);
    await context.SaveChangesAsync();
    Console.WriteLine("FINISHED: AddOrdersData");
}

async Task AddCustomersData(CustomersDbContext context)
{
    Console.WriteLine("STARTED: AddCustomersData");
    await context.AddRangeAsync(data.Customers);
    await context.AddRangeAsync(data.Contacts);
    await context.AddRangeAsync(data.Addresses);

    await context.SaveChangesAsync();
    Console.WriteLine("FINISHED: AddCustomersData");
}

async Task AddStaffData(StaffDbContext context)
{
    Console.WriteLine("STARTED: AddStaffData");
    await context.AddRangeAsync(data.Employees);
    await context.AddRangeAsync(data.Waiters);
    await context.AddRangeAsync(data.Managers);

    await context.SaveChangesAsync();
    Console.WriteLine("FINISHED: AddStaffData");
}

Console.WriteLine("FINISHED");

#endregion