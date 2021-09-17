using AW.UI.Web.Infrastructure.ApiClients.BasketApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AW.UI.Web.Infrastructure.ApiClients.BasketApi
{
    public class BasketApiClient : IBasketApiClient
    {
        private readonly HttpClient client;
        private readonly ILogger logger;

        public BasketApiClient(HttpClient client, ILogger<BasketApiClient> logger) =>
            (this.client, this.logger) = (client, logger);

        public Task Checkout(BasketCheckout basket)
        {
            throw new NotImplementedException();
        }

        public async Task<Basket> GetBasket(string userID)
        {
            string requestUri = $"/basket-api/Basket/{userID}?api-version=1.0";
            logger.LogInformation("Getting basket");

            using var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            return string.IsNullOrEmpty(responseString) ?
                new Basket { BuyerId = userID } :
                JsonSerializer.Deserialize<Basket>(responseString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
        }

        public async Task<Basket> UpdateBasket(Basket basket)
        {
            string requestUri = "/basket-api/Basket?api-version=1.0";

            var basketContent = new StringContent(JsonSerializer.Serialize(basket), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(requestUri, basketContent);
            response.EnsureSuccessStatusCode();

            return basket;
        }
    }
}