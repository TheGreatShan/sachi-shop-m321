namespace stock;

public abstract class InformationResult<T>
{
    public InformationResultType Status { get; set; }
    public T? Data { get; set; }

    public InformationResult(InformationResultType status, T data)
    {
        Status = status;
        Data = data;
    }
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