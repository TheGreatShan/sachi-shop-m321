using System.Data;
using Microsoft.Data.SqlClient;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

namespace stock;

public class ApiModule
{
    public void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();

        services.AddDiscoveryClient(builder.Configuration);
        services.AddServiceDiscovery(options => options.UseEureka());

        services.AddSingleton<IDbConnection>(_ => SqlConnection.GetConnection());
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IProductRepository, ProductRepository>();

        services.AddTransient<IInformationRepository, InformationRepository>();
        services.AddTransient<IInformationService, InformationService>();
    }
}