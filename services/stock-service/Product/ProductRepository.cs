using System.Data;
using Dapper;

namespace stock;

public interface IProductRepository
{
    ProductRecord CreateProduct(ProductInput product);
    ProductRecord UpdateProduct(Guid id, ProductInput product);
    void DeleteProduct(Guid id);
}

public class ProductRepository(IDbConnection dbConnection) : IProductRepository
{
    public ProductRecord CreateProduct(ProductInput product)
    {
        var dbProduct = new ProductRecord(Guid.NewGuid(), product.Product, product.Description, product.Stock);
        dbConnection.Query(
            "INSERT INTO Product (id, product, description, stock) VALUES (@Id, @Product, @Description, @Stock)",
            new
            {
                Id = dbProduct.Id, Product = dbProduct.Product, Description = dbProduct.Description,
                Stock = dbProduct.Stock
            });
        return dbProduct;
    }

    public ProductRecord UpdateProduct(Guid id, ProductInput product)
    {
        dbConnection.Execute(
            "UPDATE Product SET product = @Product, description = @Description, stock = @Stock WHERE id = @Id",
            new
            {
                Product = product.Product, Description = product.Description, Stock = product.Stock, Id = id
            });
        return new ProductRecord(id, product.Product, product.Description, product.Stock);
    }

    public void DeleteProduct(Guid id)
    {
        dbConnection.Execute("DELETE FROM Product WHERE id = @Id", new {Id = id});
    }
}