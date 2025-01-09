using Microsoft.AspNetCore.Mvc;

namespace stock;

public class ProductController(IProductService productService) : Controller
{
    [HttpGet("/products/{id}")]
    public ActionResult<ProductInformationPayload> GetProductById(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest();
        var product = productService.GetProductById(id).ToPayload();
        return product == null ? NotFound() : Ok(product);
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

    [HttpDelete("/products/{id}")]
    public ActionResult UpdateProduct(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest();

        productService.DeleteProduct(id);

        return Ok();
    }


    private static bool IsProductInputValid(ProductInput product) =>
        product == null || string.IsNullOrEmpty(product.Product) || string.IsNullOrEmpty(product.Description) ||
        product.Stock <= 0;
}