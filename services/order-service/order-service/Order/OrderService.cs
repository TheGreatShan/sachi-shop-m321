using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Steeltoe.Discovery;
using Steeltoe.Discovery.Eureka;

namespace order_service.Order;

public interface IOrderService
{
    Task<OrderResult<Order>> CreateOrder(OrderInput orderInput);
}

public class OrderService
    (IOrderRepository orderRepository, IDiscoveryClient discoveryClient, HttpClient client) : IOrderService
{
    public async Task<OrderResult<Order>> CreateOrder(OrderInput orderInput)
    {
        foreach (var productId in orderInput.ProductIds)
        {
            var discoveryProduct = await Inventory.Inventory.GetProductFromInventory(productId, discoveryClient, client);
            if (discoveryProduct == null)
                return new NotFound<Order>($"The following product id does not exist: {productId}");
            if (discoveryProduct.Product.Stock <= 0)
                return new Conflict<Order>($"The following product is out of stock: {productId}");
        }

        var order = await orderRepository.CreateOrder(orderInput);
        return new Ok<Order>(order);
    }
}