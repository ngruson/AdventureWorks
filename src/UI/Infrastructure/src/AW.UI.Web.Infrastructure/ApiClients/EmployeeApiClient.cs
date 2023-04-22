using System.Text.Json.Serialization;
using System.Text.Json;
using AW.UI.Web.SharedKernel.Employee.Handlers.GetEmployees;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using Microsoft.Extensions.Logging;
using System.Text;
using AW.UI.Web.SharedKernel.Employee.Handlers.AddDepartmentHistory;
using AW.UI.Web.SharedKernel.Employee.Handlers.DeleteDepartmentHistory;
using AW.UI.Web.SharedKernel.Employee.Handlers.UpdateDepartmentHistory;

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

        public async Task<SharedKernel.Employee.Handlers.GetEmployee.Employee?> GetEmployee(string loginID)
        {
            string requestUri = $"/employee-api/Employee/{loginID}?api-version=1.0";
            _logger.LogInformation("Getting employee from API");

            using var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<SharedKernel.Employee.Handlers.GetEmployee.Employee?>(
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

        public async Task<List<string>?> GetJobTitles()
        {
            string requestUri = $"/employee-api/Employee/jobTitles?api-version=1.0";
            _logger.LogInformation("Getting job titles from API");

            using var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<string>>(
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

        public async Task<SharedKernel.Employee.Handlers.UpdateEmployee.Employee?> UpdateEmployee(string key, SharedKernel.Employee.Handlers.UpdateEmployee.Employee employee)
        {
            _logger.LogInformation("Call Employee API to update employee");
            string requestUri = $"Employee/{key}?&api-version=1.0";
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(employee, options);
            _logger.LogInformation("Calling PUT method on {RequestUri} with {JSON}", requestUri, json);

            using var response = await _client.PutAsync(
                requestUri,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var updatedEmployee = await stream.DeserializeAsync<SharedKernel.Employee.Handlers.UpdateEmployee.Employee?>(options);

            _logger.LogInformation("Returning updated employee", updatedEmployee);
            return updatedEmployee;
        }

        public async Task AddDepartmentHistory(AddDepartmentHistoryCommand command)
        {
            _logger.LogInformation("Call Employee API to add department history");
            string requestUri = $"Employee/departmentHistory?&api-version=1.0";
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(command, options);
            _logger.LogInformation("Calling POST method on {RequestUri} with {JSON}", requestUri, json);

            using var response = await _client.PostAsync(
                requestUri,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateDepartmentHistory(UpdateDepartmentHistoryCommand command)
        {
            _logger.LogInformation("Call Employee API to update department history");
            string requestUri = $"Employee/departmentHistory?&api-version=1.0";
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
        }

        public async Task DeleteDepartmentHistory(DeleteDepartmentHistoryCommand command)
        {
            _logger.LogInformation("Call Employee API to delete department history");
            string requestUri = $"Employee/departmentHistory?" +
                $"loginID={command.LoginID}" +
                $"&departmentName={command.DepartmentName}" +
                $"&shiftName={command.ShiftName}" +
                $"&startDate={command.StartDate}" +
                $"&api-version=1.0";

            _logger.LogInformation("Calling DELETE method on {RequestUri}", requestUri);

            using var response = await _client.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }
    }
}
