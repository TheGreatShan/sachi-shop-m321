using System.Data;
using Dapper;

namespace StockService.Product;

public interface IProductRepository
{
    ProductRecord CreateProduct(ProductInput product);
}

public class ProductRepository(IDbConnection dbConnection) : IProductRepository
{
    public ProductRecord CreateProduct(ProductInput product)
    {
        var dbProduct = new ProductRecord(Guid.NewGuid(), product.Product, product.Description, product.Stock);
        dbConnection.Query(
            "INSERT INTO Product (id, product, description, stock) VALUES (@Id, @Product, @Description, @Stock)",
            new {Id = dbProduct.Id, Product = dbProduct.Product, Description = dbProduct.Description, Stock = dbProduct.Stock});
        return dbProduct;
    }
}