using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core;
using AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries;
using AW.SharedKernel.UnitTesting;
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
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ListCountries_ReturnsCountries(
            List<Country> countries,
            [Frozen] Mock<IMediator> mockMediator,
            CountryRegionService sut
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                It.IsAny<GetCountriesQuery>(), 
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(countries);

            //Act
            var result = await sut.ListCountries();

            //Assert
            result.Should().NotBeNull();
            result.Countries.Count().Should().Be(countries.Count);
        }
    }
}