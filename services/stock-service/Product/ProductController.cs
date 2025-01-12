using Microsoft.AspNetCore.Mvc;

namespace stock;

public class ProductController(IProductService productService) : Controller
{
    [HttpGet("/products")]
    public ActionResult<ProductInformationPayload> GetAllProducts()
    {
        var product = productService.GetAllProducts().ToPayload();
        return product == null ? NotFound() : Ok(product);
    }

    [HttpGet("/products/{id}")]
    public ActionResult<ProductInformationPayload> GetProductById(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest();
        var product = productService.GetProductById(id);
        return product == null ? NotFound() : Ok(product.ToPayload());
    }

    [HttpPost("/products")]
    public ActionResult CreateProduct([FromBody] ProductInput product)
    {
        if (IsProductInputValid(product))
            return BadRequest();
        return Ok(productService.CreateProduct(product));
    }


    [HttpPut("/products/{id}")]
    public ActionResult UpdateProduct(Guid id, [FromBody] ProductInput product)
    {
        if (id == Guid.Empty || IsProductInputValid(product))
            return BadRequest();

        var updatedProduct = productService.UpdateProduct(id, product);
        return Ok(updatedProduct);
    }

    [HttpPut("/products/stock/{id}")]
    public ActionResult<ProductRecord> DecreaseStockByOne(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest();

        var updatedProduct = productService.DecreaseStockByOne(id);

        return updatedProduct;
    }

    [HttpDelete("/products/{id}")]
    public ActionResult UpdateProduct(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest();

        productService.DeleteProduct(id);

        return NoContent();
    }


    private static bool IsProductInputValid(ProductInput product) =>
        product == null || string.IsNullOrEmpty(product.Product) || string.IsNullOrEmpty(product.Description) ||
        product.Stock <= 0;
}