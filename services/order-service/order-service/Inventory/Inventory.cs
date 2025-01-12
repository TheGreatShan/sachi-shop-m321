using System.Text.Json;
using order_service.Order;
using Steeltoe.Discovery;
using Steeltoe.Discovery.Eureka;

namespace order_service.Inventory;

public class Inventory
{
    public static async Task<ProductInformation?> GetProductFromInventory(Guid productId, IDiscoveryClient discoveryClient,
        HttpClient client)
    {
        var instances = discoveryClient.GetInstances("stock-service");
        if (instances == null || !instances.Any())
        {
            throw new Exception("No instances of stock-service found");
        }

        var stockServiceUri = instances.First().Uri;
        var response = await client.GetAsync($"{stockServiceUri}products/{productId}");

        var content = await response.Content.ReadAsStringAsync();

        return string.IsNullOrEmpty(content) ? null : JsonSerializer.Deserialize<ProductInformation>(content, new JsonSerializerOptions(){PropertyNameCaseInsensitive = true});
    }
}