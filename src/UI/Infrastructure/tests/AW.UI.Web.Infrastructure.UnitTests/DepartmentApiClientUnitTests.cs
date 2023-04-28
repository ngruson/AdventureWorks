using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients;
using FluentAssertions;
using RichardSzalay.MockHttp;
using Xunit;

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
    }
}
