using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients;
using AW.UI.Web.SharedKernel.Employee.Handlers.GetEmployees;
using FluentAssertions;
using RichardSzalay.MockHttp;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests
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
                List<Employee> employees,
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
                Func<Task> func = async () => await sut.GetEmployees();

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }
    }
}
