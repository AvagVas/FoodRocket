using FoodRocket.DBContext.Models;
using FoodRocket.DBContext.Models.Inventory;
using FoodRocket.DBContext.Models.Orders;
using FoodRocket.DBContext.Models.Staff;
using Microsoft.EntityFrameworkCore;

namespace FoodRocket.DBContext.Contexts;

public class StaffDbContext : DbContext
{ 
    public StaffDbContext(DbContextOptions<StaffDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Waiter> Waiters { get; set; }
    public DbSet<Manager> Managers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("staff");
    }
}