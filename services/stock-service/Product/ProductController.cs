using Microsoft.AspNetCore.Mvc;

namespace stock;

public class ProductController(IProductService productService) : Controller
{
    [HttpGet("/products")]
    public string GetAllProducts()
    {
        return "Get all products";
    }

    [HttpPost("/products")]
    public ProductRecord CreateProduct([FromBody] ProductInput product)
    {
        return productService.CreateProduct(product);
    }
}