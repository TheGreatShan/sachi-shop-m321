using Microsoft.AspNetCore.Mvc;

namespace order_service;

public record Info(string Message);

public class InfoController : Controller
{
    [HttpGet("/info")]
    public Info GetInfo()
    {
        return new Info("This is the Sachi Online Shop Order Service. It manages everything related to orders.");
    }
}