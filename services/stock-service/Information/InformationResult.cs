namespace stock;

public abstract class InformationResult<T>(InformationResultType status, T data)
{
    public InformationResultType Status { get; set; } = status;
    public T? Data { get; set; } = data;
}

public class BadRequest<T>() : InformationResult<T>(InformationResultType.BadRequest, default);

public class NotFound<T>() : InformationResult<T>(InformationResultType.NotFound, default);
public class Ok<T>(T data = default) : InformationResult<T>(InformationResultType.Ok, data);
public class Conflict<T>() : InformationResult<T>(InformationResultType.Conflict, default);

public enum InformationResultType
{
    BadRequest,
    NotFound,
    Ok,
    Conflict
}