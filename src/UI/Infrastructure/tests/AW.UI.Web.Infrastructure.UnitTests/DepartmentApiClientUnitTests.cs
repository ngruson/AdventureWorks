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
    }
}
