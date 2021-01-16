using AW.Core.Abstractions.Api.SalesPersonApi.GetSalesPerson;
using AW.Core.Abstractions.Api.SalesPersonApi.ListSalesPersons;
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
    public class SalesPersonApiUnitTests
    {
        [Fact]
        public async void ListSalesPersons_ReturnsSalesPersons()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<SalesPersonApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new ListSalesPersonsResponse
                {
                    SalesPersons = new List<Core.Abstractions.Api.SalesPersonApi.ListSalesPersons.SalesPerson>
                    {
                        new Core.Abstractions.Api.SalesPersonApi.ListSalesPersons.SalesPerson { FullName = "Stephen Y Jiang" },
                        new Core.Abstractions.Api.SalesPersonApi.ListSalesPersons.SalesPerson { FullName = "Michael G Blythe" }
                    }
                })
            });

            var baseAddress = "BaseAddress";

            var sut = new SalesPersonApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            var response = await sut.ListSalesPersonsAsync(new ListSalesPersonsRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()));
            response.SalesPersons[0].FullName.Should().Be("Stephen Y Jiang");
            response.SalesPersons[1].FullName.Should().Be("Michael G Blythe");
        }

        [Fact]
        public async void GetSalesPerson_ReturnsSalesPerson()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<SalesPersonApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new GetSalesPersonResponse
                {
                    SalesPerson = new Core.Abstractions.Api.SalesPersonApi.GetSalesPerson.SalesPerson
                    {
                        FullName = "Stephen Y Jiang"
                    }
                })
            });

            var baseAddress = "BaseAddress";

            var sut = new SalesPersonApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            var response = await sut.GetSalesPersonAsync(new GetSalesPersonRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<string>()));

            response.SalesPerson.FullName.Should().Be("Stephen Y Jiang");
        }
    }
}