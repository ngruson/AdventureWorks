using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System.Text;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.UpdateDepartment;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.DeleteDepartment;

namespace AW.UI.Web.Infrastructure.Api.ApiClients
{
    public class DepartmentApiClient : IDepartmentApiClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<DepartmentApiClient?> _logger;

        public DepartmentApiClient(HttpClient client, ILogger<DepartmentApiClient?> logger) =>
            (_client, _logger) = (client, logger);

        public async Task<List<Department.Handlers.GetDepartments.Department>?> GetDepartments()
        {
            var requestUri = $"/department-api/Department?api-version=1.0";
            _logger.LogInformation("Getting departments from API");

            using var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<Department.Handlers.GetDepartments.Department>?>(
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

        public async Task<Department.Handlers.GetDepartment.Department?> GetDepartment(Guid objectId)
        {
            var requestUri = $"/department-api/Department/{objectId}?api-version=1.0";
            _logger.LogInformation("Getting department from API");

            using var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<Department.Handlers.GetDepartment.Department>(
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

        public async Task<Department.Handlers.CreateDepartment.Department?> CreateDepartment(Department.Handlers.CreateDepartment.Department department)
        {
            _logger.LogInformation("Call Department API to create department");
            var requestUri = $"Department?&api-version=1.0";
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(department, options);
            _logger.LogInformation("Calling POST method on {RequestUri}", requestUri);

            using var response = await _client.PostAsync(
                requestUri,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var createdDepartment = await stream.DeserializeAsync<Department.Handlers.CreateDepartment.Department?>(options);

            _logger.LogInformation("Returning department");
            return createdDepartment;
        }

        public async Task<Department.Handlers.UpdateDepartment.Department?> UpdateDepartment(Department.Handlers.UpdateDepartment.Department department)
        {
            _logger.LogInformation("Call Department API to update department");
            var requestUri = "Department?&api-version=1.0";
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(department, options);
            _logger.LogInformation("Calling PUT method on {RequestUri} with {JSON}", requestUri, json);

            using var response = await _client.PutAsync(
                requestUri,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var updatedDepartment = await stream.DeserializeAsync<Department.Handlers.UpdateDepartment.Department?>(options);

            _logger.LogInformation("Returning updated department", updatedDepartment);
            return updatedDepartment;
        }

        public async Task DeleteDepartment(DeleteDepartmentCommand request)
        {
            _logger.LogInformation("Deleting department");
            var requestUri = $"Department/{request.ObjectId}?&api-version=1.0";
            _logger.LogInformation("Calling DELETE method on {RequestUri}", requestUri);

            using var response = await _client.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();

            _logger.LogInformation("Department succesfully deleted");
        }
    }
}
