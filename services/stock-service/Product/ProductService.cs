using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace stock;

public interface IProductService
{
    List<ProductInformation> GetAllProducts();
    ProductInformation GetProductById(Guid id);
    ProductRecord CreateProduct(ProductInput product);
    ProductRecord UpdateProduct(Guid id, ProductInput product);
    ActionResult<ProductRecord> DecreaseStockByOne(Guid id);
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

    public ActionResult<ProductRecord> DecreaseStockByOne(Guid id)
    {
        var product = GetProductById(id);

        if (product == null)
            return new NotFoundResult();

        return product.Product.Stock > 0
            ? new OkObjectResult(productRepository.UpdateProduct(id,
                product.Product.ToProductInput() with { Stock = product.Product.Stock - 1 }))
            : new BadRequestResult();
    }

    public void DeleteProduct(Guid id) =>
        productRepository.DeleteProduct(id);
}