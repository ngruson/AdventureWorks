using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using RichardSzalay.MockHttp;
using Xunit;
using AW.UI.Web.Infrastructure.Api.ApiClients;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.AddDepartmentHistory;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.DeleteDepartmentHistory;
using UpdateEmployee = AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateEmployee;
using GetEmployee = AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployee;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateDepartmentHistory;
using GetEmployees = AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployees;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.ApiClients
{
    public class EmployeeApiClientUnitTests
    {
        public class GetEmployees
        {
            [Theory, MockHttpData]
            public async Task ReturnEmployeesGivenEmployeesExist(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                List<Infrastructure.Api.Employee.Handlers.GetEmployees.Employee> employees,
                EmployeeApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(employees, new JsonSerializerOptions
                            {
                                Converters =
                                {
                                    new JsonStringEnumConverter()
                                },
                                IgnoreReadOnlyProperties = true,
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            })
                        )
                    );

                //Act
                var response = await sut.GetEmployees();

                //Assert
                response?.Should().BeEquivalentTo(employees);
            }

            [Theory, MockHttpData]
            public async Task ThrowsHttpRequestExceptionWhenEmployeesDoNotExist(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                EmployeeApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = sut.GetEmployees;

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetEmployee
        {
            [Theory, MockHttpData]
            public async Task ReturnsEmployeeWhenEmployeeExists(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                EmployeeApiClient sut,
                Infrastructure.Api.Employee.Handlers.GetEmployee.Employee employee
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(employee, new JsonSerializerOptions
                            {
                                Converters =
                                {
                                    new JsonStringEnumConverter()
                                },
                                IgnoreReadOnlyProperties = true,
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            })
                        )
                    );

                //Act
                var response = await sut.GetEmployee(employee.ObjectId);

                //Assert
                response.Should().BeEquivalentTo(employee);
            }

            [Theory, MockHttpData]
            public async Task ThrowsHttpRequestExceptionWhenEmployeeIsNotFound(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                EmployeeApiClient sut,
                Guid objectId
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.GetEmployee(objectId);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetJobTitles
        {
            [Theory, MockHttpData]
            public async Task ReturnJobTitlesGivenJobTitlesExist(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                List<string> jobTitles,
                EmployeeApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(jobTitles, new JsonSerializerOptions
                            {
                                Converters =
                                {
                                    new JsonStringEnumConverter()
                                },
                                IgnoreReadOnlyProperties = true,
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            })
                        )
                    );

                //Act
                var response = await sut.GetJobTitles();

                //Assert
                response?.Should().BeEquivalentTo(jobTitles);
            }

            [Theory, MockHttpData]
            public async Task ThrowsHttpRequestExceptionWhenJobTitlesDoNotExist(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                EmployeeApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = sut.GetJobTitles;

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class UpdateEmployee
        {
            [Theory, MockHttpData]
            public async Task ReturnUpdatedEmployeeGivenEmployee(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                EmployeeApiClient sut,
                Infrastructure.Api.Employee.Handlers.UpdateEmployee.Employee employee
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Put, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(employee, new JsonSerializerOptions
                            {
                                Converters =
                                {
                                    new JsonStringEnumConverter()
                                },
                                IgnoreReadOnlyProperties = true,
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            })
                        )
                    );

                //Act
                var response = await sut.UpdateEmployee(employee);

                //Assert
                response.Should().BeEquivalentTo(employee);
            }

            [Theory, MockHttpData]
            public async Task ThrowHttpRequestExceptionGivenEmployeeNotFound(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                EmployeeApiClient sut,
                Infrastructure.Api.Employee.Handlers.UpdateEmployee.Employee employee
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Put, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.UpdateEmployee(employee);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class AddDepartmentHistory
        {
            [Theory, MockHttpData]
            public async Task ApiReturnsOk(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                EmployeeApiClient sut,
                AddDepartmentHistoryCommand command
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Post, $"{uri}*")
                    .Respond(HttpStatusCode.OK);

                //Act
                await sut.AddDepartmentHistory(command);

                //Assert
            }

            [Theory, MockHttpData]
            public async Task ThrowHttpRequestExceptionGivenEmployeeNotFound(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                EmployeeApiClient sut,
                AddDepartmentHistoryCommand command
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Post, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.AddDepartmentHistory(command);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class UpdateDepartmentHistory
        {
            [Theory, MockHttpData]
            public async Task ApiReturnsOk(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                EmployeeApiClient sut,
                UpdateDepartmentHistoryCommand command
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Put, $"{uri}*")
                    .Respond(HttpStatusCode.OK);

                //Act
                await sut.UpdateDepartmentHistory(command);

                //Assert
            }

            [Theory, MockHttpData]
            public async Task ThrowHttpRequestExceptionGivenEmployeeNotFound(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                EmployeeApiClient sut,
                UpdateDepartmentHistoryCommand command
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Put, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.UpdateDepartmentHistory(command);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class DeleteDepartmentHistory
        {
            [Theory, MockHttpData]
            public async Task ApiReturnsOk(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                EmployeeApiClient sut,
                DeleteDepartmentHistoryCommand command
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Delete, $"{uri}*")
                    .Respond(HttpStatusCode.OK);

                //Act
                await sut.DeleteDepartmentHistory(command);

                //Assert
            }

            [Theory, MockHttpData]
            public async Task ThrowHttpRequestExceptionGivenEmployeeNotFound(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                EmployeeApiClient sut,
                DeleteDepartmentHistoryCommand command
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Delete, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.DeleteDepartmentHistory(command);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }
    }
}
