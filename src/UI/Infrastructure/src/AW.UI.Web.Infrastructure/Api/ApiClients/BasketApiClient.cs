using AW.UI.Web.Infrastructure.Api.Basket.Handlers.Checkout;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AW.UI.Web.Infrastructure.Api.ApiClients
{
    public class BasketApiClient : IBasketApiClient
    {
        private readonly HttpClient client;
        private readonly ILogger logger;

        public BasketApiClient(HttpClient client, ILogger<BasketApiClient?> logger) =>
            (this.client, this.logger) = (client, logger);

        public async Task CheckoutAsync(BasketCheckout? basket)
        {
            var requestUri = "Basket/checkout?api-version=1.0";

            var basketContent = new StringContent(JsonSerializer.Serialize(basket), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUri, basketContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Basket.Handlers.GetBasket.Basket?> GetBasketAsync(string? userID)
        {
            var requestUri = $"Basket/{userID}?api-version=1.0";
            logger.LogInformation("Getting basket");

            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            return string.IsNullOrEmpty(responseString) ?
                new Basket.Handlers.GetBasket.Basket { BuyerId = userID } :
                JsonSerializer.Deserialize<Basket.Handlers.GetBasket.Basket>(responseString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
        }

        public async Task<Basket.Handlers.UpdateBasket.Basket?> UpdateBasketAsync(Basket.Handlers.UpdateBasket.Basket? basket)
        {
            var requestUri = "Basket?api-version=1.0";

            var basketContent = new StringContent(JsonSerializer.Serialize(basket), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUri, basketContent);
            response.EnsureSuccessStatusCode();

            return basket;
        }
    }
}
