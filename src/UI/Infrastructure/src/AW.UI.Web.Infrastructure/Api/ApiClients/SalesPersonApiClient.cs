using AW.UI.Web.Infrastructure.Api.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AW.UI.Web.Infrastructure.Api.ApiClients
{
    public class SalesPersonApiClient : ISalesPersonApiClient
    {
        private readonly HttpClient client;
        private readonly ILogger<SalesPersonApiClient?> logger;

        public SalesPersonApiClient(HttpClient client, ILogger<SalesPersonApiClient?> logger) =>
            (this.client, this.logger) = (client, logger);

        public async Task<List<SalesPerson.Handlers.GetSalesPersons.SalesPerson>?> GetSalesPersonsAsync(string? territory = null)
        {
            logger.LogInformation("Getting sales persons");
            var requestUri = "SalesPerson?api-version=1.0";

            if (!string.IsNullOrEmpty(territory))
                requestUri += $"&territory={territory}";

            using var response = await client.GetAsync(requestUri);
            var stream = await response.Content.ReadAsStreamAsync();
            response.EnsureSuccessStatusCode();

            return await stream.DeserializeAsync<List<SalesPerson.Handlers.GetSalesPersons.SalesPerson>?>(new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                IgnoreReadOnlyProperties = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task<SalesPerson.Handlers.GetSalesPerson.SalesPerson?> GetSalesPersonAsync(string firstName, string? middleName, string lastName)
        {
            logger.LogInformation("Getting sales person");
            var requestUri = "SalesPerson?api-version=1.0";

            if (!string.IsNullOrEmpty(firstName))
                requestUri += $"&firstName={firstName}";

            if (!string.IsNullOrEmpty(middleName))
                requestUri += $"&middleName={middleName}";

            if (!string.IsNullOrEmpty(lastName))
                requestUri += $"&lastName={lastName}";

            using var response = await client.GetAsync(requestUri);
            var stream = await response.Content.ReadAsStreamAsync();
            response.EnsureSuccessStatusCode();

            return await stream.DeserializeAsync<SalesPerson.Handlers.GetSalesPerson.SalesPerson?>(new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                IgnoreReadOnlyProperties = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
