using System.Data;
using Microsoft.EntityFrameworkCore;
using order_service.Db;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

namespace order_service;

public class ApiModule
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<MariaDbContext>(options =>
        {
            // TODO move to appsettings
            var connectionString =
                "Server=localhost,3306;Database=order;Uid=root;Pwd=humanities@best-password.ByFar2025;";
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString));
        });
        services.AddControllers();
        services.AddServiceDiscovery(options => options.UseEureka());
    }
}