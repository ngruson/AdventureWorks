using System.Text.Json.Serialization;
using System.Text.Json;
using AW.UI.Web.SharedKernel.Employee.Handlers.GetEmployees;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using Microsoft.Extensions.Logging;

namespace AW.UI.Web.Infrastructure.ApiClients
{
    public class EmployeeApiClient : IEmployeeApiClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<EmployeeApiClient?> _logger;

        public EmployeeApiClient(HttpClient client, ILogger<EmployeeApiClient?> logger) =>
            (_client, _logger) = (client, logger);

        public async Task<List<Employee>?> GetEmployees()
        {
            string requestUri = $"/employee-api/Employee?api-version=1.0";
            _logger.LogInformation("Getting employees from API");

            using var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<Employee>?>(
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
