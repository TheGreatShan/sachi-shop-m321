namespace stock;

public interface IProductService
{
    List<ProductInformation> GetAllProducts();
    ProductInformation GetProductById(Guid id);
    ProductRecord CreateProduct(ProductInput product);
    ProductRecord UpdateProduct(Guid id, ProductInput product);
    void DeleteProduct(Guid id);
}


public class ProductService(IProductRepository productRepository) : IProductService
{
    public List<ProductInformation> GetAllProducts() =>
        productRepository.GetAllProducts();

    public ProductInformation GetProductById(Guid id) =>
        productRepository.GetProductById(id);

    public ProductRecord CreateProduct(ProductInput product) =>
        productRepository.CreateProduct(product);

    public ProductRecord UpdateProduct(Guid id, ProductInput product) =>
        productRepository.UpdateProduct(id, product);

    public void DeleteProduct(Guid id) =>
        productRepository.DeleteProduct(id);
}