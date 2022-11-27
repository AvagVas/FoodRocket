using FoodRocket.DBContext.Models;
using FoodRocket.DBContext.Models.Inventory;
using FoodRocket.DBContext.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace FoodRocket.DBContext.Contexts;

public class OrdersDbContext : DbContext
{ 
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options) { }

    public DbSet<Menu> Menus { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<PriceOffer> PriceOffers { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();
 
        foreach (var entry in ChangeTracker.Entries())
        {
            if (typeof(DishMenu).ToString() == entry.Entity.ToString())
            {
                if(entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    //entry.Property("MenuId1").CurrentValue = entry.Property("MenuId").CurrentValue;
                    //entry.Property("MenuVersion").CurrentValue = entry.Property("Version").CurrentValue;

                }
            }

        }
        return base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("orders");

        modelBuilder.Entity<Menu>() 
            .HasKey(x => new {x.MenuId, x.Version});
        
        modelBuilder.Entity<DishMenu>() 
            .HasKey(x => new {x.MenuId, x.Version, x.DishId});

        modelBuilder.Entity<IngredientDish>() 
            .HasKey(x => new {x.IngredientId, x.DishId});
    }
}