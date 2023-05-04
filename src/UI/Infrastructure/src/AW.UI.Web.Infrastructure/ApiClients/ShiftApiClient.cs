using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Shift.Handlers.GetShifts;
using System.Text;
using AW.UI.Web.SharedKernel.Shift.Handlers.DeleteShift;

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

        public async Task<SharedKernel.Shift.Handlers.GetShift.Shift?> GetShift(string name)
        {
            string requestUri = $"/shift-api/Shift/{name}?api-version=1.0";
            _logger.LogInformation("Getting shift from API");

            using var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<SharedKernel.Shift.Handlers.GetShift.Shift>(
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

        public async Task<SharedKernel.Shift.Handlers.CreateShift.Shift?> CreateShift(SharedKernel.Shift.Handlers.CreateShift.Shift shift)
        {
            _logger.LogInformation("Call Shift API to create shift");
            string requestUri = $"Shift?&api-version=1.0";
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(shift, options);
            _logger.LogInformation("Calling POST method on {RequestUri}", requestUri);

            using var response = await _client.PostAsync(
                requestUri,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var createdShift = await stream.DeserializeAsync<SharedKernel.Shift.Handlers.CreateShift.Shift?>(options);

            _logger.LogInformation("Returning shift");
            return createdShift;
        }

        public async Task<SharedKernel.Shift.Handlers.UpdateShift.Shift?> UpdateShift(SharedKernel.Shift.Handlers.UpdateShift.UpdateShiftCommand command)
        {
            _logger.LogInformation("Call Shift API to update shift");
            string requestUri = $"Shift/{command.Key}?&api-version=1.0";
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(command, options);
            _logger.LogInformation("Calling PUT method on {RequestUri} with {JSON}", requestUri, json);

            using var response = await _client.PutAsync(
                requestUri,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var updatedShift = await stream.DeserializeAsync<SharedKernel.Shift.Handlers.UpdateShift.Shift?>(options);

            _logger.LogInformation("Returning updated shift", updatedShift);
            return updatedShift;
        }

        public async Task DeleteShift(DeleteShiftCommand request)
        {
            _logger.LogInformation("Deleting shift");
            string requestUri = $"Shift/{request.Name}?&api-version=1.0";
            _logger.LogInformation("Calling DELETE method on {RequestUri}", requestUri);

            using var response = await _client.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();

            _logger.LogInformation("Shift succesfully deleted");
        }
    }
}
