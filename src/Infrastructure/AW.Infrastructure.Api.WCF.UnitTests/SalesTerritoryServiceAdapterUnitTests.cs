using AutoMapper;
using AW.Core.Abstractions.Api.AddressTypeApi.ListAddressTypes;
using AW.Infrastructure.Api.WCF.AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AW.Infrastructure.Api.WCF.UnitTests
{
    public class SalesTerritoryServiceAdapterUnitTests
    {
        [Fact]
        public async void ListSalesTerritories_ReturnsSalesTerritories()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<SalesTerritoryProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<SalesTerritoryServiceAdapter>>();
            var mockSalesTerritoryService = new Mock<SalesTerritoryService.ISalesTerritoryService>();
            mockSalesTerritoryService
                .Setup(x => x.ListTerritoriesAsync(It.IsAny<SalesTerritoryService.ListTerritoriesRequest>()))
                .ReturnsAsync(new SalesTerritoryService.ListTerritoriesResponse
                {
                    ListTerritoriesResult = new SalesTerritoryService.TerritoryDto[]
                    {
                        new SalesTerritoryService.TerritoryDto
                        {
                            Name = "Northwest"
                        },
                        new SalesTerritoryService.TerritoryDto
                        {
                            Name = "Northeast"
                        }
                    }
                });

            var sut = new SalesTerritoryServiceAdapter(
                mockLogger.Object,
                mapper,
                mockSalesTerritoryService.Object
            );

            //Act
            var response = await sut.ListTerritoriesAsync();

            //Assert
            mockSalesTerritoryService.Verify(x => x.ListTerritoriesAsync(It.IsAny<SalesTerritoryService.ListTerritoriesRequest>()));
            response.Territories[0].Name.Should().Be("Northwest");
            response.Territories[1].Name.Should().Be("Northeast");
        }
    }
}