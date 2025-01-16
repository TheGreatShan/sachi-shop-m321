using Microsoft.AspNetCore.Mvc;

namespace order_service.Order;

public class OrderController(IOrderService service) : ControllerBase
{
    [HttpPost("/order")]
    public async Task<ActionResult<Order>> CreateNewOrder([FromBody] OrderInput order)
    {
        var createdOrder = await service.CreateOrder(order with{DateTime = DateTime.Now});
        return createdOrder.Status switch
        {
            OrderResultType.Ok => Ok(createdOrder.Data),
            OrderResultType.NotFound => NotFound(createdOrder.Message),
            OrderResultType.Conflict => Conflict(createdOrder.Message),
            OrderResultType.BadRequest => BadRequest(createdOrder.Message),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [HttpGet("/order/{id}")]
    public async Task<ActionResult<OrderPayload>> GetOrderById(Guid id)
    {
        var createdOrder = await service.GetOrderById(id);
        return createdOrder.Status switch
        {
            OrderResultType.Ok => Ok(createdOrder.Data),
            OrderResultType.NotFound => NotFound(createdOrder.Message),
            OrderResultType.Conflict => Conflict(createdOrder.Message),
            OrderResultType.BadRequest => BadRequest(createdOrder.Message),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [HttpGet("/order/email/{email}")]
    public async Task<ActionResult<List<OrderPayload>>> GetOrderByEmail(string email)
    {
        var createdOrder = await service.GetOrderByEmail(email);
        return createdOrder.Status switch
        {
            OrderResultType.Ok => Ok(createdOrder.Data),
            OrderResultType.NotFound => NotFound(createdOrder.Message),
            OrderResultType.Conflict => Conflict(createdOrder.Message),
            OrderResultType.BadRequest => BadRequest(createdOrder.Message),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [HttpDelete("/order/{id}")]
    public async Task<ActionResult<bool>> DeleteOrderById(Guid id)
    {
        var createdOrder = await service.DeleteOrderById(id);
        return createdOrder.Status switch
        {
            OrderResultType.NotFound => NotFound(createdOrder.Message),
            OrderResultType.Conflict => Conflict(createdOrder.Message),
            OrderResultType.BadRequest => BadRequest(createdOrder.Message),
            OrderResultType.NoContent => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}