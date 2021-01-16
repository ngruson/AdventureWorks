using AW.Core.Abstractions.Api.StateProvinceApi.ListStateProvinces;
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
    public class StateProvinceApiUnitTests
    {
        [Fact]
        public async void ListStateProvinces_ReturnsStateProvinces()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<StateProvinceApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new ListStateProvincesResponse
                {
                    StateProvinces = new List<StateProvince>
                    {
                        new StateProvince { Name = "California"},
                        new StateProvince { Name = "Washington"}
                    }
                })
            });

            var baseAddress = "BaseAddress";

            var sut = new StateProvinceApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            var response = await sut.ListStateProvincesAsync(new ListStateProvincesRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()));
            response.StateProvinces[0].Name.Should().Be("California");
            response.StateProvinces[1].Name.Should().Be("Washington");
        }
    }
}