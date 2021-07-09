using AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries;
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
    public class CountryRegionServiceUnitTests
    {
        [Fact]
        public async Task ListContactTypes_ReturnsContactTypes()
        {
            //Arrange
            var countries = new List<Country>
            {
                new Country { CountryRegionCode = "US", Name = "United States" },
                new Country { CountryRegionCode = "GB", Name = "United Kingdom" }
            };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetCountriesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(countries);

            var contactTypeService = new CountryRegionService(
                mockMediator.Object
            );

            //Act
            var result = await contactTypeService.ListCountries();

            //Assert
            result.Should().NotBeNull();
            result.Countries.Count().Should().Be(2);
        }
    }
}