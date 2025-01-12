using Microsoft.EntityFrameworkCore;

namespace order_service.Db;

public class MariaDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Order.Order> Order { get; set; }
    public DbSet<Order.ProductOrder> ProductOrders { get; set; }
};
