using FoodRocket.DBContext.Models;
using FoodRocket.DBContext.Models.Inventory;
using Microsoft.EntityFrameworkCore;

namespace FoodRocket.DBContext.Contexts;

public class InventoryDbContext : DbContext
{ 
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
    public DbSet<StorageProduct> ProductsInStorages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("inventory");

        modelBuilder.Entity<StorageProduct>() 
            .HasKey(x => new {x.StorageId, x.ProductId});
    }
}