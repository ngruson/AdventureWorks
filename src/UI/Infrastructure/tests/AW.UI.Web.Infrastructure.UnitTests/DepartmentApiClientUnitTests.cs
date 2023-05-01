using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients;
using FluentAssertions;
using RichardSzalay.MockHttp;
using Xunit;
using AW.UI.Web.SharedKernel.Department.Handlers.DeleteDepartment;

namespace AW.UI.Web.Infrastructure.UnitTests
{
    public class DepartmentApiClientUnitTests
    {
        public class GetDepartments
        {
            [Theory, MockHttpData]
            public async Task ReturnDepartmentsGivenDepartmentsExist(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                List<SharedKernel.Department.Handlers.GetDepartments.Department> departments,
                DepartmentApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(departments, new JsonSerializerOptions
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
                var response = await sut.GetDepartments();

                //Assert
                response?.Should().BeEquivalentTo(departments);
            }

            [Theory, MockHttpData]
            public async Task ThrowsHttpRequestExceptionWhenDepartmentsDoNotExist(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                DepartmentApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = sut.GetDepartments;

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class GetDepartment
        {
            [Theory, MockHttpData]
            public async Task ReturnsDepartmentWhenDepartmentExists(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                DepartmentApiClient sut,
                SharedKernel.Department.Handlers.GetDepartment.Department department
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(department, new JsonSerializerOptions
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
                var response = await sut.GetDepartment(department.Name!);

                //Assert
                response.Should().BeEquivalentTo(department);
            }

            [Theory, MockHttpData]
            public async Task ThrowsHttpRequestExceptionWhenDepartmentIsNotFound(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                DepartmentApiClient sut,
                string name
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.GetDepartment(name);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class CreateDepartment
        {
            [Theory, MockHttpData]
            public async Task return_updated_department_given_department(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                DepartmentApiClient sut,
                SharedKernel.Department.Handlers.CreateDepartment.CreateDepartmentCommand command
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Post, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(command.Department, new JsonSerializerOptions
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
                var response = await sut.CreateDepartment(command.Department);

                //Assert
                response.Should().BeEquivalentTo(command.Department);
            }

            [Theory, MockHttpData]
            public async Task throw_httprequestexception_given_department_not_found(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                DepartmentApiClient sut,
                SharedKernel.Department.Handlers.CreateDepartment.CreateDepartmentCommand command
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Post, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.CreateDepartment(command.Department);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class UpdateDepartment
        {
            [Theory, MockHttpData]
            public async Task ReturnUpdatedDepartmentGivenDepartment(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                DepartmentApiClient sut,
                SharedKernel.Department.Handlers.UpdateDepartment.UpdateDepartmentCommand command
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Put, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(command.Department, new JsonSerializerOptions
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
                var response = await sut.UpdateDepartment(command);

                //Assert
                response.Should().BeEquivalentTo(command.Department);
            }

            [Theory, MockHttpData]
            public async Task ThrowHttpRequestExceptionGivenDepartmentNotFound(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                DepartmentApiClient sut,
                SharedKernel.Department.Handlers.UpdateDepartment.UpdateDepartmentCommand command
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Put, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.UpdateDepartment(command);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class DeleteDepartment
        {
            [Theory, MockHttpData]
            public async Task delete_department_ok(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                DepartmentApiClient sut,
                DeleteDepartmentCommand command
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Delete, $"{uri}*")
                    .Respond(HttpStatusCode.OK);

                //Act
                await sut.DeleteDepartment(command);

                //Assert
                1.Should().Be(1);
            }

            [Theory, MockHttpData]
            public async Task throw_httprequestexception_given_department_does_not_exist(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                DepartmentApiClient sut,
                DeleteDepartmentCommand command
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Delete, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.DeleteDepartment(command);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }
    }
}
