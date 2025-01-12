using Microsoft.AspNetCore.Mvc;
using order_service.Db;

namespace order_service.Order;

public class OrderController(IOrderRepository repository) : Controller
{
    [HttpPost("/order")]
    public async Task<ActionResult<Order>> CreateNewOrder([FromBody] OrderInput order)
    {
        var createdOrder = await repository.CreateOrder(order);
        return Ok(createdOrder);
    }
}