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

        var informationList = informations
            .Select(information => information.ToInformation())
            .ToList();

        return informationList;
    }

    internal static InformationRecord ToInformation(this DbInformation information)
    {
        if (information == null)
            return null;

        return new InformationRecord(information.Id, information.ProductId, information.Information,
            Enum.Parse<Stage>(information.Stage));
    }

    internal static List<InformationPayload> ToPayload(this List<InformationRecord> information)
    {
        if (information == null || information.Count == 0)
            return null;

        return information
            .Select(x =>
                new InformationPayload(x.Id, x.ProductId, x.Information, x.Stage.ToString()))
            .ToList();
    }
}