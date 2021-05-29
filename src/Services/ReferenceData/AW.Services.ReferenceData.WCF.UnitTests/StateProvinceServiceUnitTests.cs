using AW.Services.ReferenceData.Application.StateProvince.GetStatesProvinces;
using AW.Services.ReferenceData.WCF.Messages.ListStatesProvinces;
using FluentAssertions;
using MediatR;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.WCF.UnitTests
{
    public class StateProvinceServiceUnitTests
    {
        [Fact]
        public async Task ListStateProvinces_ReturnsStateProvinces()
        {
            //Arrange
            var countries = new List<StateProvince>
            {
                new StateProvince { CountryRegionCode = "US", StateProvinceCode = "NY", Name = "New York" },
                new StateProvince { CountryRegionCode = "US", StateProvinceCode = "CA", Name = "California" }
            };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetStatesProvincesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(countries);

            var stateProvinceService = new StateProvinceService(
                mockMediator.Object
            );

            //Act
            var request = new ListStatesProvincesRequest();
            var result = await stateProvinceService.ListStatesProvinces(request);

            //Assert
            result.Should().NotBeNull();
            result.StateProvinces.Count().Should().Be(2);
        }
    }
}