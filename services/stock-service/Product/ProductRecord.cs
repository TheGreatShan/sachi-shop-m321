namespace stock;

public record ProductRecord(Guid Id, string Product, string Description, int Stock);

public record ProductInformation(ProductRecord Product, List<InformationRecord> Informations);

public record ProductInformationPayload(ProductRecord Product, List<InformationPayload> Informations);

public record ProductInput(string Product, string Description, int Stock);

internal static class ProductExtensions
{
    public static ProductInformation ToProductInformation(this ProductRecord product,
        List<InformationRecord> informations) =>
        new(product, informations);

    public static ProductInformationPayload ToPayload(this ProductInformation product) =>
        new(product.Product, product.Informations.Select(information =>
            new InformationPayload(information.Id, information.ProductId, information.Information,
                information.Stage.ToString())).ToList());

    public static List<ProductInformationPayload> ToPayload(this List<ProductInformation> products) =>
        products.Select(products => products.ToPayload()).ToList();
    public static ProductInput ToProductInput(this ProductRecord product) =>
        new (product.Product, product.Description, product.Stock);
}