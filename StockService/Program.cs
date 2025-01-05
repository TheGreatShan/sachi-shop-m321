using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddServiceDiscovery(options => options.UseEureka());

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