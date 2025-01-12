using Microsoft.EntityFrameworkCore;
using order_service.Db;
using order_service.Inventory;

namespace order_service.Order;

public interface IOrderRepository
{
    Task<Order> CreateOrder(OrderInput orderInput);
    Task<OrderPayload> GetOrderById(Guid id);
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

    public async Task<OrderPayload> GetOrderById(Guid id)
    {
        var order = await mariaDbContext.Order
            .Where(o => o.Id == id)
            .Select(o => new OrderPayload(
                o.Id,
                o.Email,
                o.DateTime,
                mariaDbContext.ProductOrders
                    .Where(p => p.OrderId == o.Id)
                    .Select(p => p.ProductId)
                    .ToList()
            ))
            .FirstOrDefaultAsync();


        return order;
    }
}