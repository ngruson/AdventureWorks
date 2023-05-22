using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System.Text;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.AddDepartmentHistory;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.DeleteDepartmentHistory;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateDepartmentHistory;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.DeleteEmployee;

namespace AW.UI.Web.Infrastructure.Api.ApiClients
{
    public class EmployeeApiClient : IEmployeeApiClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<EmployeeApiClient?> _logger;

        public EmployeeApiClient(HttpClient client, ILogger<EmployeeApiClient?> logger) =>
            (_client, _logger) = (client, logger);

        public async Task<List<Employee.Handlers.GetEmployees.Employee>?> GetEmployees()
        {
            var requestUri = $"/employee-api/Employee?api-version=1.0";
            _logger.LogInformation("Getting employees from API");

            using var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<List<Employee.Handlers.GetEmployees.Employee>?>(
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

        public async Task<Employee.Handlers.GetEmployee.Employee?> GetEmployee(Guid objectId)
        {
            var requestUri = $"/employee-api/Employee/{objectId}?api-version=1.0";
            _logger.LogInformation("Getting employee from API");

            using var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();

            return await stream.DeserializeAsync<Employee.Handlers.GetEmployee.Employee?>(
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
            var requestUri = $"/employee-api/JobTitles?api-version=1.0";
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

        public async Task<Employee.Handlers.CreateEmployee.Employee?> CreateEmployee(Employee.Handlers.CreateEmployee.Employee employee)
        {
            _logger.LogInformation("Call Employee API to create employee");
            var requestUri = $"Employee?&api-version=1.0";
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(employee, options);
            _logger.LogInformation("Calling POST method on {RequestUri}", requestUri);

            using var response = await _client.PostAsync(
                requestUri,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var createdEmployee = await stream.DeserializeAsync<Employee.Handlers.CreateEmployee.Employee?>(options);

            _logger.LogInformation("Returning department");
            return createdEmployee;
        }

        public async Task<Employee.Handlers.UpdateEmployee.UpdatedEmployee?> UpdateEmployee(Employee.Handlers.UpdateEmployee.Employee employee)
        {
            _logger.LogInformation("Call Employee API to update employee");
            var requestUri = "Employee/?&api-version=1.0";
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(employee, options);
            _logger.LogInformation("Calling PUT method on {RequestUri} with {JSON}", requestUri, json);

            using var response = await _client.PutAsync(
                requestUri,
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            var updatedEmployee = await stream.DeserializeAsync<Employee.Handlers.UpdateEmployee.UpdatedEmployee?>(options);

            _logger.LogInformation("Returning updated employee", updatedEmployee);
            return updatedEmployee;
        }

        public async Task DeleteEmployee(DeleteEmployeeCommand request)
        {
            _logger.LogInformation("Deleting employee");
            var requestUri = $"Employee/{request.ObjectId}?&api-version=1.0";
            _logger.LogInformation("Calling DELETE method on {RequestUri}", requestUri);

            using var response = await _client.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();

            _logger.LogInformation("Employee succesfully deleted");
        }

        public async Task AddDepartmentHistory(AddDepartmentHistoryCommand command)
        {
            _logger.LogInformation("Call Employee API to add department history");
            var requestUri = $"Employee/departmentHistory?&api-version=1.0";
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(command, options);
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
            var requestUri = $"Employee/departmentHistory?&api-version=1.0";
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(command, options);
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
            var requestUri = $"Employee/departmentHistory?" +
                $"employee={command.Employee}" +
                $"&objectId={command.ObjectId}" +
                $"&api-version=1.0";

            _logger.LogInformation("Calling DELETE method on {RequestUri}", requestUri);

            using var response = await _client.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }
    }
}
