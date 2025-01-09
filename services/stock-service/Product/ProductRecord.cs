namespace stock;

public record ProductRecord(Guid Id, string Product, string Description, int Stock);

public record ProductInformation(ProductRecord Product, List<InformationRecord> Informations);

public record ProductInput(string Product, string Description, int Stock);

public record InformationRecord(Guid Id, Guid ProductId, string Information, string Stage);

public static class ProductExtensions
{
    public static ProductInformation ToProductInformation(this ProductRecord product,
        List<InformationRecord> informations) =>
        new(product, informations);
}