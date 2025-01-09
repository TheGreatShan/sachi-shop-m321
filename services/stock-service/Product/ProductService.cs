namespace stock;

public interface IProductService
{
    ProductRecord CreateProduct(ProductInput product);
    ProductRecord UpdateProduct(Guid id, ProductInput product);
    void DeleteProduct(Guid id);
}


public class ProductService(IProductRepository productRepository) : IProductService
{
    public ProductRecord CreateProduct(ProductInput product) =>
        productRepository.CreateProduct(product);

    public ProductRecord UpdateProduct(Guid id, ProductInput product) =>
        productRepository.UpdateProduct(id, product);

    public void DeleteProduct(Guid id) =>
        productRepository.DeleteProduct(id);
}