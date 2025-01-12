namespace order_service.Order;

public enum OrderResultType
{
    Ok,
    NotFound,
    Conflict
}

public abstract class OrderResult<T>(OrderResultType status, T data = default, string Message = default)
{
    public OrderResultType Status { get; } = status;
    public T Data { get; } = data;
    public string Message { get; } = Message;
}

public class Ok<T>(T data) : OrderResult<T>(OrderResultType.Ok, data);

public class NotFound<T>(string message) : OrderResult<T>(OrderResultType.NotFound, default, message);
public class Conflict<T>(string message) : OrderResult<T>(OrderResultType.Conflict, default, message);