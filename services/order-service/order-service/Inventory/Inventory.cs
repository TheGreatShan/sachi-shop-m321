using System.Net;
using System.Net.Http;
using System.Text.Json;
using order_service.Order;
using Polly;
using Polly.CircuitBreaker;
using Steeltoe.Discovery;
using Steeltoe.Discovery.Eureka;

namespace order_service.Inventory;

public class Inventory
{
    private static readonly AsyncCircuitBreakerPolicy CircuitBreakerPolicy = Policy
        .Handle<HttpRequestException>()
        .CircuitBreakerAsync(2, TimeSpan.FromMinutes(1));

    public static async Task<ProductInformation?> GetProductFromInventory(Guid productId, IDiscoveryClient discoveryClient,
        HttpClient client)
    {
        return await CircuitBreakerPolicy.ExecuteAsync(async () =>
        {
            var instances = discoveryClient.GetInstances("stock-service");
            if (instances == null || !instances.Any())
                throw new Exception("No instances of stock-service found");

            var stockServiceUri = instances.First().Uri;
            var response = await client.GetAsync($"{stockServiceUri}products/{productId}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return string.IsNullOrEmpty(content) ? null : JsonSerializer.Deserialize<ProductInformation>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        });
    }
}