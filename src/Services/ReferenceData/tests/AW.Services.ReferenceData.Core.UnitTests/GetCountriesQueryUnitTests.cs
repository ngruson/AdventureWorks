using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Exceptions;
using AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
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
        public async Task Handle_NoCountriesExists_ThrowCountriesNotFoundException(
            [Frozen] Mock<IRepository<Entities.CountryRegion>> countryRegionRepoMock,
            GetCountriesQueryHandler sut,
            GetCountriesQuery query
        )
        {
            //Arrange
            countryRegionRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Entities.CountryRegion>());

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<CountriesNotFoundException>();
            countryRegionRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
        }
    }
}