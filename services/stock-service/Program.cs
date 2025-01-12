using Steeltoe.Discovery.Client;
using stock;

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