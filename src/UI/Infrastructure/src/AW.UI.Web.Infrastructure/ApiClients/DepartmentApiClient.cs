using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using System.Text;
using AW.UI.Web.SharedKernel.Department.Handlers.DeleteDepartment;

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

        public async Task<SharedKernel.Department.Handlers.CreateDepartment.Department?> CreateDepartment(SharedKernel.Department.Handlers.CreateDepartment.Department department)
        {
            _logger.LogInformation("Call Department API to create department");
            string requestUri = $"Department?&api-version=1.0";
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(department, options);
            _logger.LogInformation("Calling POST method on {RequestUri}", requestUri);

            using var response = await _client.PostAsync(
                requestUri,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var createdDepartment = await stream.DeserializeAsync<SharedKernel.Department.Handlers.CreateDepartment.Department?>(options);

            _logger.LogInformation("Returning department");
            return createdDepartment;
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

        public async Task DeleteDepartment(DeleteDepartmentCommand request)
        {
            _logger.LogInformation("Deleting department");
            string requestUri = $"Department/{request.Name}?&api-version=1.0";
            _logger.LogInformation("Calling DELETE method on {RequestUri}", requestUri);

            using var response = await _client.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();

            _logger.LogInformation("Department succesfully deleted");
        }
    }
}
