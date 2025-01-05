using Microsoft.AspNetCore.Mvc;

namespace StockService.Product;

public class ProductController : Controller
{
    [HttpGet("/products")]
    public string GetAllProducts()
    {
        return "Get all products";
    }
}