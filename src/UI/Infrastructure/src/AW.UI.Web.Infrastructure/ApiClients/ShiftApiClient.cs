using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Shift.Handlers.GetShifts;

namespace AW.UI.Web.Infrastructure.ApiClients
{
    public class ShiftApiClient : IShiftApiClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<ShiftApiClient?> _logger;

        public ShiftApiClient(HttpClient client, ILogger<ShiftApiClient?> logger) =>
            (_client, _logger) = (client, logger);

        public async Task<List<Shift>?> GetShifts()
        {
            string requestUri = $"/shift-api/Shift?api-version=1.0";
            _logger.LogInformation("Getting shifts from API");

            using var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<Shift>?>(
                new JsonSerializerOptions
                {
                    Converters =
                    {
                        new JsonStringEnumConverter()
                    },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
        }
    }
}
