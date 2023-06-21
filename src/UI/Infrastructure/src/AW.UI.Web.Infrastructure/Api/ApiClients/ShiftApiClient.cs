using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System.Text;
using AW.UI.Web.Infrastructure.Api.Interfaces;

namespace AW.UI.Web.Infrastructure.Api.ApiClients;

public class ShiftApiClient : IShiftApiClient
{
    private readonly HttpClient _client;
    private readonly ILogger<ShiftApiClient?> _logger;

    public ShiftApiClient(HttpClient client, ILogger<ShiftApiClient?> logger) =>
        (_client, _logger) = (client, logger);

    public async Task<List<Shift.Handlers.GetShifts.Shift>?> GetShifts()
    {
        var requestUri = $"/shift-api/Shift?api-version=1.0";
        _logger.LogInformation("Getting shifts from API");

        using var response = await _client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();

        return await stream.DeserializeAsync<List<Shift.Handlers.GetShifts.Shift>?>(
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

    public async Task<Shift.Handlers.GetShift.Shift?> GetShift(Guid objectId)
    {
        var requestUri = $"/shift-api/Shift/{objectId}?api-version=1.0";
        _logger.LogInformation("Getting shift from API");

        using var response = await _client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();

        return await stream.DeserializeAsync<Shift.Handlers.GetShift.Shift>(
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

    public async Task<Shift.Handlers.CreateShift.CreatedShift?> CreateShift(Shift.Handlers.CreateShift.Shift shift)
    {
        _logger.LogInformation("Call Shift API to create shift");
        var requestUri = $"Shift?&api-version=1.0";
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(shift, options);
        _logger.LogInformation("Calling POST method on {RequestUri}", requestUri);

        using var response = await _client.PostAsync(
            requestUri,
            new StringContent(json, Encoding.UTF8, "application/json")
        );
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var createdShift = await stream.DeserializeAsync<Shift.Handlers.CreateShift.CreatedShift?>(options);

        _logger.LogInformation("Returning shift");
        return createdShift;
    }

    public async Task<Shift.Handlers.UpdateShift.Shift?> UpdateShift(Shift.Handlers.UpdateShift.Shift shift)
    {
        _logger.LogInformation("Call Shift API to update shift");
        var requestUri = $"Shift?&api-version=1.0";
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(shift, options);
        _logger.LogInformation("Calling PUT method on {RequestUri} with {JSON}", requestUri, json);

        using var response = await _client.PutAsync(
            requestUri,
            new StringContent(json, Encoding.UTF8, "application/json")
        );
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var updatedShift = await stream.DeserializeAsync<Shift.Handlers.UpdateShift.Shift?>(options);

        _logger.LogInformation("Returning updated shift", updatedShift);
        return updatedShift;
    }

    public async Task DeleteShift(Guid objectId)
    {
        _logger.LogInformation("Deleting shift");
        var requestUri = $"Shift/{objectId}?&api-version=1.0";
        _logger.LogInformation("Calling DELETE method on {RequestUri}", requestUri);

        using var response = await _client.DeleteAsync(requestUri);
        response.EnsureSuccessStatusCode();

        _logger.LogInformation("Shift succesfully deleted");
    }
}
