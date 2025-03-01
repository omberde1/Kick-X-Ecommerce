using Microsoft.EntityFrameworkCore;

namespace Kick_X.Models;

public class MyDBContext : DbContext 
{
    public MyDBContext(DbContextOptions options) : base(options) { }

    public DbSet<Admin> tbl_admin { get; set; }
    public DbSet<Customer> tbl_customer { get; set; }

    public DbSet<Category> tbl_category { get; set; }
    public DbSet<Product> tbl_product { get; set; }

    public DbSet<CartItem> tbl_cartitem { get; set; }
    public DbSet<Order> tbl_order { get; set; }
    public DbSet<OrderItem> tbl_orderitem { get; set; }
}