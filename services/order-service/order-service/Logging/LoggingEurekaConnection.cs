using System.Net;
using System.Text.Json;
using order_service.Inventory;
using Polly;
using Polly.CircuitBreaker;
using Steeltoe.Discovery;

namespace order_service.Logging;

public class LoggingEurekaConnection
{
    private static readonly AsyncCircuitBreakerPolicy CircuitBreakerPolicy = Policy
        .Handle<HttpRequestException>()
        .CircuitBreakerAsync(2, TimeSpan.FromMinutes(1));

    public static async Task LogMessage(Logging logging,
        IDiscoveryClient discoveryClient,
        HttpClient client)
    {
        await CircuitBreakerPolicy.ExecuteAsync(async () =>
        {
            var instances = discoveryClient.GetInstances("event-hub-service");
            if (instances == null || !instances.Any())
                throw new Exception("No instances of logging-service found");

            var eventHubUrl = instances.First().Uri;
            var serializedJson = JsonSerializer.Serialize(logging,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower });
            var response = await client.PostAsync($"{eventHubUrl}produce?topic=logs",
                new StringContent(serializedJson));

            response.EnsureSuccessStatusCode();
        });
    }
}