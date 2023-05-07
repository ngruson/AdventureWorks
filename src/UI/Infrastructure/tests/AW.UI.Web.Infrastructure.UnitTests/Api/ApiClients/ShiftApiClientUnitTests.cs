using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using RichardSzalay.MockHttp;
using Xunit;
using AW.UI.Web.Infrastructure.Api.ApiClients;
using AW.UI.Web.Infrastructure.Api.Shift.Handlers.CreateShift;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.ApiClients
{
    public class ShiftApiClientUnitTests
    {
        public class GetShifts
        {
            [Theory, MockHttpData]
            public async Task return_shifts_given_shifts_exist(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                List<Infrastructure.Api.Shift.Handlers.GetShifts.Shift> shifts,
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
            public async Task throw_httprequestexception_given_shifts_not_found(
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

        public class GetShift
        {
            [Theory, MockHttpData]
            public async Task return_shift_given_shift_exists(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ShiftApiClient sut,
                Infrastructure.Api.Shift.Handlers.GetShift.Shift expected
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(expected, new JsonSerializerOptions
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
                var actual = await sut.GetShift(expected.ObjectId);

                //Assert
                actual.Should().BeEquivalentTo(expected);
            }

            [Theory, MockHttpData]
            public async Task throw_httprequestexception_given_shift_is_not_found(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ShiftApiClient sut,
                Guid objectId
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.GetShift(objectId);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class CreateShift
        {
            [Theory, MockHttpData]
            public async Task return_updated_shift_given_shift(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ShiftApiClient sut,
                CreateShiftCommand command
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Post, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(command.Shift, new JsonSerializerOptions
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
                var actual = await sut.CreateShift(command.Shift);

                //Assert
                actual.Should().BeEquivalentTo(command.Shift);
            }

            [Theory, MockHttpData]
            public async Task throw_httprequestexception_given_shift_not_found(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ShiftApiClient sut,
                CreateShiftCommand command
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Post, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.CreateShift(command.Shift);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class UpdateShift
        {
            [Theory, MockHttpData]
            public async Task return_updated_shift_given_shift(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ShiftApiClient sut,
                Infrastructure.Api.Shift.Handlers.UpdateShift.Shift expected
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Put, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(expected, new JsonSerializerOptions
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
                var actual = await sut.UpdateShift(expected);

                //Assert
                actual.Should().BeEquivalentTo(expected);
            }

            [Theory, MockHttpData]
            public async Task throw_httprequestexception_given_shift_not_found(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ShiftApiClient sut,
                Infrastructure.Api.Shift.Handlers.UpdateShift.Shift shift
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Put, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.UpdateShift(shift);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class DeleteShift
        {
            [Theory, MockHttpData]
            public async Task delete_shift_ok(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ShiftApiClient sut,
                Guid objectId
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Delete, $"{uri}*")
                    .Respond(HttpStatusCode.OK);

                //Act
                await sut.DeleteShift(objectId);

                //Assert
                1.Should().Be(1);
            }

            [Theory, MockHttpData]
            public async Task throw_httprequestexception_given_shift_does_not_exist(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                ShiftApiClient sut,
                Guid objectId
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;
                handler.When(HttpMethod.Delete, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.DeleteShift(objectId);

                //Assert
                await func.Should().ThrowAsync<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }
    }
}
