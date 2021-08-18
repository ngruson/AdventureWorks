using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.Core.UnitTests
{
    public class GetCountriesQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_CountriesExists_ReturnCountries(
            List<Entities.CountryRegion> countries,
            [Frozen] Mock<IRepository<Entities.CountryRegion>> countryRegionRepoMock,
            GetCountriesQueryHandler sut,
            GetCountriesQuery query
        )
        {
            //Arrange
            countryRegionRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(countries);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            countryRegionRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
            
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Name.Should().Be(countries[i].Name);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void Handle_NoCountriesExists_ThrowException(
            [Frozen] Mock<IRepository<Entities.CountryRegion>> countryRegionRepoMock,
            GetCountriesQueryHandler sut,
            GetCountriesQuery query
        )
        {
            //Arrange
            countryRegionRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<Entities.CountryRegion>)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>();
            countryRegionRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
        }
    }
}