using System.Data;
using Dapper;
using Microsoft.VisualBasic;

namespace stock;

public interface IProductRepository
{
    ProductInformation GetProductById(Guid id);
    List<ProductInformation> GetAllProducts();
    ProductRecord CreateProduct(ProductInput product);
    ProductRecord UpdateProduct(Guid id, ProductInput product);
    void DeleteProduct(Guid id);
}

public class ProductRepository(IDbConnection dbConnection) : IProductRepository
{
    public ProductInformation GetProductById(Guid id)
    {
        var product = dbConnection.Query<ProductRecord>(
            @"SELECT
                        p.id AS Id, 
                        p.product AS Product,
                        p.description AS Description,
                        p.stock AS Stock
                    FROM Product p  
                    WHERE p.id = @Id ",
            new {Id = id}).FirstOrDefault();

        return GetProductInformation(product);

    }
    public List<ProductInformation> GetAllProducts()
    {
        var product = dbConnection.Query<ProductRecord>(
            @"SELECT
                        p.id AS Id, 
                        p.product AS Product,
                        p.description AS Description,
                        p.stock AS Stock
                    FROM Product p").ToList();



        return product?.Select(GetProductInformation).ToList();
    }

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
        dbConnection.Execute("DELETE FROM Information WHERE productId = @Id", new {Id = id});
        dbConnection.Execute("DELETE FROM Product WHERE id = @Id", new {Id = id});
    }

    private ProductInformation GetProductInformation(ProductRecord p)
    {
        var informations = dbConnection.Query<DbInformation>(@"SELECT
                        i.id AS Id, 
                        i.productId AS ProductId,
                        i.information AS Information,
                        i.stage AS Stage
                    FROM Information i  
                    WHERE i.productId = @Id ", new { p.Id })
            .ToList();

        return p.ToProductInformation(informations.ToInformation());
    }
}