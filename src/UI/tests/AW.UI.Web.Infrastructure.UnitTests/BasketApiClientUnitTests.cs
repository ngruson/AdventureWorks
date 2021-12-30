using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients.BasketApi;
using AW.UI.Web.Infrastructure.ApiClients.BasketApi.Models;
using FluentAssertions;
using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests
{
    public class BasketApiClientUnitTests
    {
        public class GetBasket
        {
            [Theory, MockHttpData]
            public async Task GetBasket_BasketExist_ReturnsBasket(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                BasketApiClient sut,
                Basket basket
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(basket, new JsonSerializerOptions
                            {
                                Converters =
                                    {
                                        new JsonStringEnumConverter()
                                    },
                                IgnoreReadOnlyProperties = true,
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            }),
                            Encoding.UTF8,
                            "application/json"
                        )
                    );

                //Act
                var response = await sut.GetBasket(basket.BuyerId);

                //Assert
                response.BuyerId.Should().Be(basket.BuyerId);
                response.Items.Count.Should().Be(basket.Items.Count);
            }

            [Theory, MockHttpData]
            public void GetBasket_NoBasketFound_ThrowsHttpRequestException(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                BasketApiClient sut,
                string userID
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Get, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.GetBasket(userID);

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class UpdateBasket
        {
            [Theory, MockHttpData]
            public async Task UpdateBasket_BasketExist_ReturnsBasket(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                BasketApiClient sut,
                Basket basket
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Post, $"{uri}*")
                    .Respond(HttpStatusCode.OK,
                        new StringContent(
                            JsonSerializer.Serialize(basket, new JsonSerializerOptions
                            {
                                Converters =
                                    {
                                        new JsonStringEnumConverter()
                                    },
                                IgnoreReadOnlyProperties = true,
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            }),
                            Encoding.UTF8,
                            "application/json"
                        )
                    );

                //Act
                var response = await sut.UpdateBasket(basket);

                //Assert
                response.BuyerId.Should().Be(basket.BuyerId);
                response.Items.Count.Should().Be(basket.Items.Count);
            }

            [Theory, MockHttpData]
            public void UpdateBasket_NoBasketFound_ThrowsHttpRequestException(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                BasketApiClient sut,
                Basket basket
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Post, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.UpdateBasket(basket);

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }

        public class Checkout
        {
            [Theory, MockHttpData]
            public void Checkout_ReturnsAccepted(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                BasketApiClient sut,
                BasketCheckout basket
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Post, $"{uri}*")
                    .Respond(HttpStatusCode.Accepted);

                //Act
                async Task func() => await sut.Checkout(basket);

                //Assert
                var task = func();
                task.GetAwaiter().GetResult();
                task.IsCompleted.Should().BeTrue();
            }

            [Theory, MockHttpData]
            public void Checkout_Fails_ThrowsHttpRequestException(
                [Frozen] MockHttpMessageHandler handler,
                [Frozen] HttpClient httpClient,
                Uri uri,
                BasketApiClient sut,
                BasketCheckout basket
            )
            {
                //Arrange
                httpClient.BaseAddress = uri;

                handler.When(HttpMethod.Post, $"{uri}*")
                    .Respond(HttpStatusCode.NotFound);

                //Act
                Func<Task> func = async () => await sut.Checkout(basket);

                //Assert
                func.Should().Throw<HttpRequestException>()
                    .WithMessage("Response status code does not indicate success: 404 (Not Found).");
            }
        }
    }
}