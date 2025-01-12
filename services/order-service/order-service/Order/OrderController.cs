using Microsoft.AspNetCore.Mvc;
using order_service.Db;

namespace order_service.Order;

public class OrderController(MariaDbContext mariaDbContext) : Controller
{

}