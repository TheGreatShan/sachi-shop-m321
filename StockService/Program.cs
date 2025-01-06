using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using StockService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var apiModule = new ApiModule();
        apiModule.ConfigureServices(builder.Services);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseDiscoveryClient();
        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}