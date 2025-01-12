using Microsoft.EntityFrameworkCore;
using order_service.Db;

namespace order_service.Order;

public interface IOrderRepository
{
    Task<Order> CreateOrder(OrderInput orderInput);
}

public class OrderRepository(MariaDbContext mariaDbContext) : IOrderRepository
{
    public async Task<Order> CreateOrder(OrderInput orderInput)
    {
        var id = Guid.NewGuid();
        var order = orderInput.ToOrder(id);

        mariaDbContext.Add(order);
        await mariaDbContext.SaveChangesAsync();

        var productOrders = orderInput.ToProductOrder(id);
        mariaDbContext.AddRange(productOrders);
        await mariaDbContext.SaveChangesAsync();

        return order;
    }
}