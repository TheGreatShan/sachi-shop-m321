using System.Data;
using Microsoft.Data.SqlClient;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using StockService.Product;
using StockService.Utils;
using SqlConnection = StockService.Utils.SqlConnection;

namespace StockService;

public class ApiModule
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();
        services.AddServiceDiscovery(options => options.UseEureka());

        services.AddSingleton<IDbConnection>(_ => SqlConnection.GetConnection());
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IProductRepository, ProductRepository>();
    }
}