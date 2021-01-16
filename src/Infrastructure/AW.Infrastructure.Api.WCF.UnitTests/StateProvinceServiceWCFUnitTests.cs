using AutoMapper;
using AW.Core.Abstractions.Api.StateProvinceApi.ListStateProvinces;
using AW.Infrastructure.Api.WCF.AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AW.Infrastructure.Api.WCF.UnitTests
{
    public class StateProvinceServiceWCFUnitTests
    {
        [Fact]
        public async void ListStateProvinces_ReturnsStateProvinces()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<StateProvinceProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<StateProvinceServiceWCF>>();
            var mockStateProvinceService = new Mock<StateProvinceService.IStateProvinceService>();
            mockStateProvinceService
                .Setup(x => x.ListStateProvincesAsync(It.IsAny<StateProvinceService.ListStateProvincesRequest>()))
                .ReturnsAsync(new StateProvinceService.ListStateProvincesResponse
                {
                    StateProvinces = new StateProvinceService.StateProvince[]
                    {
                        new StateProvinceService.StateProvince
                        {
                            Name = "Arizona"
                        },
                        new StateProvinceService.StateProvince
                        {
                            Name = "Washington"
                        }
                    }
                });

            var sut = new StateProvinceServiceWCF(
                mockLogger.Object,
                mapper,
                mockStateProvinceService.Object
            );

            //Act
            var response = await sut.ListStateProvincesAsync(new ListStateProvincesRequest());

            //Assert
            mockStateProvinceService.Verify(x => x.ListStateProvincesAsync(It.IsAny<StateProvinceService.ListStateProvincesRequest>()));
            response.StateProvinces[0].Name.Should().Be("Arizona");
            response.StateProvinces[1].Name.Should().Be("Washington");
        }
    }
}