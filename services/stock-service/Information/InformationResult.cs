namespace stock;

public abstract class InformationResult<T>(InformationResultType status, T data = default)
{
    public InformationResultType Status { get; set; } = status;
    public T? Data { get; set; } = data;
}

public class BadRequest<T>() : InformationResult<T>(InformationResultType.BadRequest);

public class NotFound<T>() : InformationResult<T>(InformationResultType.NotFound);
public class Ok<T>(T data = default) : InformationResult<T>(InformationResultType.Ok, data);
public class Conflict<T>() : InformationResult<T>(InformationResultType.Conflict);
public class Deleted<T>() : InformationResult<T>(InformationResultType.Deleted);

public enum InformationResultType
{
    BadRequest,
    NotFound,
    Ok,
    Conflict,
    Deleted
}