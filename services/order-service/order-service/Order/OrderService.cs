using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace order_service.Order;

public interface IOrderService
{
    Task<OrderResult<Order>> CreateOrder(OrderInput orderInput);
}

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public async Task<OrderResult<Order>> CreateOrder(OrderInput orderInput)
    {
        var order = await orderRepository.CreateOrder(orderInput);
        return new Ok<Order>(order);
    }
}