using order_service;
using Steeltoe.Discovery.Client;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var apiModule = new ApiModule();
        apiModule.ConfigureServices(builder.Services, builder);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseDiscoveryClient();
        app.MapControllers();
        app.UseHttpsRedirection();

        app.Run();
    }
}