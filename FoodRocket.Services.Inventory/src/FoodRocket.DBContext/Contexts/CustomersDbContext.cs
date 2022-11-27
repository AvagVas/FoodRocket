using FoodRocket.DBContext.Models;
using FoodRocket.DBContext.Models.Customers;
using FoodRocket.DBContext.Models.Inventory;
using FoodRocket.DBContext.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace FoodRocket.DBContext.Contexts;

public class CustomersDbContext : DbContext
{ 
    public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("customers");
        
    }
}