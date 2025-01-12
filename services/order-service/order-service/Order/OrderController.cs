using Microsoft.AspNetCore.Mvc;

namespace order_service.Order;

public class OrderController(IOrderService service) : ControllerBase
{
    [HttpPost("/order")]
    public async Task<ActionResult<Order>> CreateNewOrder([FromBody] OrderInput order)
    {
        var createdOrder = await service.CreateOrder(order);
        return createdOrder.Status switch
        {
            OrderResultType.Ok => Ok(createdOrder.Data),
            OrderResultType.NotFound => NotFound(createdOrder.Message),
            OrderResultType.Conflict => Conflict(createdOrder.Message),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}