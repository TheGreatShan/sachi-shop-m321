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
        var order = orderInput.ToOrder();

        mariaDbContext.Add(order);
        await mariaDbContext.SaveChangesAsync();

        return order;
    }
}