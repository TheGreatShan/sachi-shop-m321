using Microsoft.AspNetCore.Mvc;

namespace stock;

public record Info(string Message);

public class InfoController : Controller
{
    [HttpGet("/info")]
    public Info GetInfo()
    {
        return new Info("This is the Sachi Online Shop Stock Service. It manages everything related to the store inventory.");
    }
}