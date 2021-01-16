using AutoMapper;
using AW.Core.Abstractions.Api.SalesPersonApi.GetSalesPerson;
using AW.Core.Abstractions.Api.SalesPersonApi.ListSalesPersons;
using AW.Infrastructure.Api.WCF.AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AW.Infrastructure.Api.WCF.UnitTests
{
    public class SalesPersonServiceWCFUnitTests
    {
        [Fact]
        public async void ListSalesPersons_ReturnsSalesPersons()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<SalesPersonProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<SalesPersonServiceWCF>>();
            var mockSalesPersonService = new Mock<SalesPersonService.ISalesPersonService>();
            mockSalesPersonService.Setup(x => x.ListSalesPersonsAsync(It.IsAny<SalesPersonService.ListSalesPersonsRequest1>()))
                .ReturnsAsync(new SalesPersonService.ListSalesPersonsResponse
                {
                    ListSalesPersonsResult = new SalesPersonService.SalesPersonDto[]
                    {
                        new SalesPersonService.SalesPersonDto
                        {
                            FullName = "John Doe"
                        },
                        new SalesPersonService.SalesPersonDto
                        {
                            FullName = "Jane Doe"
                        }
                    }
                });

            var sut = new SalesPersonServiceWCF(
                mockLogger.Object,
                mapper,
                mockSalesPersonService.Object
            );

            //Act
            var response = await sut.ListSalesPersonsAsync(new ListSalesPersonsRequest());

            //Assert
            mockSalesPersonService.Verify(x => x.ListSalesPersonsAsync(It.IsAny<SalesPersonService.ListSalesPersonsRequest1>()));
            response.SalesPersons[0].FullName.Should().Be("John Doe");
            response.SalesPersons[1].FullName.Should().Be("Jane Doe");
        }

        [Fact]
        public async void GetSalesPerson_ReturnsSalesPerson()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<SalesPersonProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<SalesPersonServiceWCF>>();
            var mockSalesPersonService = new Mock<SalesPersonService.ISalesPersonService>();
            mockSalesPersonService.Setup(x => x.GetSalesPersonAsync(It.IsAny<SalesPersonService.GetSalesPersonRequest>()))
                .ReturnsAsync(new SalesPersonService.GetSalesPersonResponseGetSalesPersonResult
                {
                    SalesPerson = new SalesPersonService.SalesPersonDto1
                    {
                        FullName = "John Doe"
                    }
                });

            var sut = new SalesPersonServiceWCF(
                mockLogger.Object,
                mapper,
                mockSalesPersonService.Object
            );

            //Act
            var response = await sut.GetSalesPersonAsync(new GetSalesPersonRequest());

            //Assert
            mockSalesPersonService.Verify(x => x.GetSalesPersonAsync(It.IsAny<SalesPersonService.GetSalesPersonRequest>()));
            response.SalesPerson.FullName.Should().Be("John Doe");
        }
    }
}