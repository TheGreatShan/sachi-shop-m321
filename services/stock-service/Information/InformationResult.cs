namespace stock;

public abstract class InformationResult<T>(InformationResultType status, T data)
{
    public InformationResultType Status { get; set; } = status;
    public T? Data { get; set; } = data;
}

public class BadRequest<T>(T data = default) : InformationResult<T>(InformationResultType.BadRequest, data);

public class NotFound<T>(T data = default) : InformationResult<T>(InformationResultType.NotFound, data);
public class Ok<T>(T data = default) : InformationResult<T>(InformationResultType.Ok, data);

public enum InformationResultType
{
    BadRequest,
    NotFound,
    Ok
}