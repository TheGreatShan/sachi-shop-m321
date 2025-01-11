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
    Task<bool> DoesExist(Guid id);
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
                        p.stock AS Stock,
                        CAST(p.price AS DOUBLE) AS Price
                    FROM Product p  
                    WHERE p.id = @Id ",
            new { Id = id }).FirstOrDefault();

        return GetProductInformation(RoundPrice(product));
    }

    private static ProductRecord RoundPrice(ProductRecord? product) => product with{Price = Math.Round(product.Price, 2)};

    public List<ProductInformation> GetAllProducts()
    {
        var product = dbConnection.Query<ProductRecord>(
            @"SELECT
                        p.id AS Id, 
                        p.product AS Product,
                        p.description AS Description,
                        p.stock AS Stock,
                        CAST(p.price AS DOUBLE) AS Price
                        FROM Product p").ToList();


        return product?.Select(GetProductInformation).ToList();
    }

    public ProductRecord CreateProduct(ProductInput product)
    {
        var dbProduct = new ProductRecord(Guid.NewGuid(), product.Product, product.Description, product.Stock, product.Price);
        dbConnection.Query(
            "INSERT INTO Product (id, product, description, stock, price) VALUES (@Id, @Product, @Description, @Stock, @Price)",
            new
            {
                Id = dbProduct.Id, Product = dbProduct.Product, Description = dbProduct.Description,
                Stock = dbProduct.Stock, Price = dbProduct.Price
            });
        return dbProduct;
    }

    public ProductRecord UpdateProduct(Guid id, ProductInput product)
    {
        dbConnection.Execute(
            "UPDATE Product SET product = @Product, description = @Description, stock = @Stock, price = @Price WHERE id = @Id",
            new
            {
                Product = product.Product, Description = product.Description, Stock = product.Stock, Id = id, price = product.Price
            });
        return new ProductRecord(id, product.Product, product.Description, product.Stock, product.Price);
    }

    public void DeleteProduct(Guid id)
    {
        dbConnection.Execute("DELETE FROM Information WHERE productId = @Id", new { Id = id });
        dbConnection.Execute("DELETE FROM Product WHERE id = @Id", new { Id = id });
    }

    public async Task<bool> DoesExist(Guid id)
    {
        var exists = await dbConnection.QueryAsync<Guid>("SELECT id FROM Product WHERE id = @id", new { id });
        return exists.Count() == 1;
    }

    private ProductInformation GetProductInformation(ProductRecord p)
    {
        var product = RoundPrice(p);
        var informations = dbConnection.Query<DbInformation>(@"SELECT
                        i.id AS Id, 
                        i.productId AS ProductId,
                        i.information AS Information,
                        i.stage AS Stage
                    FROM Information i  
                    WHERE i.productId = @Id ", new { product.Id })
            .ToList();

        return product.ToProductInformation(informations.ToInformation());
    }
}