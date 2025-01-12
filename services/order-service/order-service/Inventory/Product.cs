namespace order_service.Inventory;

public record ProductRecord(Guid Id, string Product, string Description, int Stock, double Price);

public record InformationPayload(Guid Id, Guid ProductId, string Information, string Stage);

public record ProductInformation(ProductRecord Product, List<InformationPayload> Informations);
