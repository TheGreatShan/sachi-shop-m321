using Steeltoe.Discovery.Client;
using stock;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var apiModule = new ApiModule();
        apiModule.ConfigureServices(builder.Services);
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "Cors Frontend",
                policy  =>
                {
                    policy.WithOrigins("http://localhost:5173",
                        "http://localhost:5173");
                });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("Cors Frontend");
        app.UseDiscoveryClient();
        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}