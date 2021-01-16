using AW.Core.Abstractions.Api.AddressTypeApi.ListAddressTypes;
using AW.Infrastructure.Http;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace AW.Infrastructure.Api.REST.UnitTests
{
    public class AddressTypeApiUnitTests
    {
        [Fact]
        public async void ListAddressTypes_ReturnsAddressTypes()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<AddressTypeApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new ListAddressTypesResponse
                {
                    AddressTypes = new List<string>
                    {
                        "Billing", "Home"
                    }
                })
            });

            var baseAddress = "BaseAddress";

            var sut = new AddressTypeApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            var response = await sut.ListAddressTypesAsync();

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()));
            response.AddressTypes[0].Should().Be("Billing");
            response.AddressTypes[1].Should().Be("Home");
        }
    }
}