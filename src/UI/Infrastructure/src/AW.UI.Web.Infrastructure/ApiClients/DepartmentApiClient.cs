using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Department.Handlers.GetDepartments;

namespace AW.UI.Web.Infrastructure.ApiClients
{
    public class DepartmentApiClient : IDepartmentApiClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<DepartmentApiClient?> _logger;

        public DepartmentApiClient(HttpClient client, ILogger<DepartmentApiClient?> logger) =>
            (_client, _logger) = (client, logger);

        public async Task<List<Department>?> GetDepartments()
        {
            string requestUri = $"/department-api/Department?api-version=1.0";
            _logger.LogInformation("Getting departments from API");

            using var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<Department>?>(
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
