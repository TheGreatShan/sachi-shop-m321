namespace StockService.Product;

public interface IProductService
{
    ProductRecord CreateProduct(ProductInput product);
}


public class ProductService(IProductRepository productRepository) : IProductService
{
    public ProductRecord CreateProduct(ProductInput product) =>
        productRepository.CreateProduct(product);
}