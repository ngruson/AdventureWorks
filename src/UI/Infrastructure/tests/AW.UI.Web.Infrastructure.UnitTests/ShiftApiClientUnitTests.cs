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
    public class ShiftApiClientUnitTests
    {
        public class GetShifts
        {
            [Theory, MockHttpData]
            public async Task ReturnShiftsGivenShiftsExist(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                List<SharedKernel.Shift.Handlers.GetShifts.Shift> shifts,
                ShiftApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(shifts, new JsonSerializerOptions
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
                var response = await sut.GetShifts();

                //Assert
                response?.Should().BeEquivalentTo(shifts);
            }

            [Theory, MockHttpData]
            public async Task ThrowsHttpRequestExceptionWhenShiftsDoNotExist(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ShiftApiClient sut
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = sut.GetShifts;

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }
    }
}
