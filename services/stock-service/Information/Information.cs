namespace stock;

public enum Stage
{
    Information,
    Warning,
    OutOfStock,
    Discontinued
}

public record DbInformation(Guid Id, Guid ProductId, string Information, string Stage);

public record InformationRecord(Guid Id, Guid ProductId, string Information, Stage Stage);
public record InformationPayload(Guid Id, Guid ProductId, string Information, string Stage);

internal static class InformationExtensions
{
    internal static List<InformationRecord> ToInformation(this List<DbInformation> informations)
    {
        if (informations == null || informations.Count == 0)
            return new List<InformationRecord>();

        var informationList = informations.Select(information =>
        {
            return new InformationRecord(information.Id, information.ProductId, information.Information,
                Enum.Parse<Stage>(information.Stage));
        }).ToList();

        return informationList;
    }
}