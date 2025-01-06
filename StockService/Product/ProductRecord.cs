namespace StockService.Product;

public record ProductRecord(Guid Id, string Product, string Description, int Stock);
public record ProductInput(string Product, string Description, int Stock);
