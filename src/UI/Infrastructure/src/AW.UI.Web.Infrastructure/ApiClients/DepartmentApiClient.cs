using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using System.Text;

namespace AW.UI.Web.Infrastructure.ApiClients
{
    public class DepartmentApiClient : IDepartmentApiClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<DepartmentApiClient?> _logger;

        public DepartmentApiClient(HttpClient client, ILogger<DepartmentApiClient?> logger) =>
            (_client, _logger) = (client, logger);

        public async Task<List<SharedKernel.Department.Handlers.GetDepartments.Department>?> GetDepartments()
        {
            string requestUri = $"/department-api/Department?api-version=1.0";
            _logger.LogInformation("Getting departments from API");

            using var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<SharedKernel.Department.Handlers.GetDepartments.Department>?>(
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

        public async Task<SharedKernel.Department.Handlers.GetDepartment.Department?> GetDepartment(string name)
        {
            string requestUri = $"/department-api/Department/{name}?api-version=1.0";
            _logger.LogInformation("Getting department from API");

            using var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<SharedKernel.Department.Handlers.GetDepartment.Department>(
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

        public async Task<SharedKernel.Department.Handlers.UpdateDepartment.Department?> UpdateDepartment(SharedKernel.Department.Handlers.UpdateDepartment.UpdateDepartmentCommand command)
        {
            _logger.LogInformation("Call Department API to update department");
            string requestUri = $"Department/{command.Key}?&api-version=1.0";
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
            var updatedDepartment = await stream.DeserializeAsync<SharedKernel.Department.Handlers.UpdateDepartment.Department?>(options);

            _logger.LogInformation("Returning updated department", updatedDepartment);
            return updatedDepartment;
        }
    }
}
