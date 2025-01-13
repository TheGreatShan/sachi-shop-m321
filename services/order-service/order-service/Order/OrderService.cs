using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using order_service.Inventory;
using order_service.Logging;
using Steeltoe.Discovery;
using Steeltoe.Discovery.Eureka;

namespace order_service.Order;

public interface IOrderService
{
    Task<OrderResult<Order>> CreateOrder(OrderInput orderInput);
    Task<OrderResult<OrderPayload>> GetOrderById(Guid id);
    Task<OrderResult<List<OrderPayload>>> GetOrderByEmail(string email);
    Task<OrderResult<bool>> DeleteOrderById(Guid id);
}

public class OrderService
    (IOrderRepository orderRepository, IDiscoveryClient discoveryClient, HttpClient client) : IOrderService
{
    public async Task<OrderResult<Order>> CreateOrder(OrderInput orderInput)
    {
        if (orderInput.ProductIds.Count == 0)
        {
            return new BadRequest<Order>("Order cannot be empty");
        }

        foreach (var productId in orderInput.ProductIds)
        {
            var discoveryProduct =
                await InventoryEurekaOps.GetProductFromInventory(productId, discoveryClient, client);
            if (discoveryProduct == null)
                return new NotFound<Order>($"The following product id does not exist: {productId}");
            if (discoveryProduct.Product.Stock <= 0)
                return new Conflict<Order>($"The following product is out of stock: {productId}");
            Thread.Sleep(250);
            InventoryEurekaOps.DecreaseStockByOne(productId, discoveryClient, client);
            Thread.Sleep(250);
        }

        var order = await orderRepository.CreateOrder(orderInput);

        var message = JsonSerializer.Serialize(order);
        await LoggingEurekaConnection.LogMessage(new Logging.Logging("INFO",
                $"This order has been created: {message}", DateTime.Now, order.Email),
            discoveryClient, client);

        return new Ok<Order>(order);
    }

    public async Task<OrderResult<OrderPayload>> GetOrderById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new BadRequest<OrderPayload>("id cannot be empty");
        }

        var order = await orderRepository.GetOrderById(id);

        return order == null
            ? new NotFound<OrderPayload>($"Could not find order with id: {id}")
            : new Ok<OrderPayload>(order);
    }

    public async Task<OrderResult<List<OrderPayload>>> GetOrderByEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return new BadRequest<List<OrderPayload>>("email cannot be empty");
        }

        var orders = await orderRepository.GetOrdersByEmail(email);

        return orders.Count == 0
            ? new NotFound<List<OrderPayload>>($"No orders for user with email: {email}")
            : new Ok<List<OrderPayload>>(orders);
    }

    public async Task<OrderResult<bool>> DeleteOrderById(Guid id)
    {
        if (id == Guid.Empty)
            return new BadRequest<bool>("id cannot be empty");

        var order = await orderRepository.GetOrderById(id);
        if (order == null)
        {
            await LoggingEurekaConnection.LogMessage(new Logging.Logging("CRITICAL",
                $"Order with id {id} cannot be deleted", DateTime.Now, "unknown"), discoveryClient, client);
            return new Conflict<bool>("Order could not be deleted");
        }

        order.ProductIds.ForEach(async x =>
        {
            Thread.Sleep(250);
            await InventoryEurekaOps.IncreaseStockByOne(x, discoveryClient, client);
        });
        var deleted = await orderRepository.DeleteOrder(id);

        if (!deleted)
        {
            await LoggingEurekaConnection.LogMessage(new Logging.Logging("CRITICAL",
                $"Order with id {id} cannot be deleted", DateTime.Now, order.Email), discoveryClient, client);
            return new Conflict<bool>("Order could not be deleted");
        }

        await LoggingEurekaConnection.LogMessage(new Logging.Logging("INFO",
            $"Order with id: {id} was deleted", DateTime.Now, order.Email), discoveryClient, client);
        return new NoContent<bool>($"Deleted entry with id: {id}");
    }
}