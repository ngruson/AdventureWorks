using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.ReferenceData.Core.UnitTests;

public class GetCountriesQueryUnitTests
{
    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_success_given_countries_exist(
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
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(countries, opt => opt
            .Excluding(_ => _.StatesProvinces)
        );

        countryRegionRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
    }

    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_notfound_given_no_countries_exist(
        [Frozen] Mock<IRepository<Entities.CountryRegion>> countryRegionRepoMock,
        GetCountriesQueryHandler sut,
        GetCountriesQuery query
    )
    {
        //Arrange
        countryRegionRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Entities.CountryRegion>());

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.Status.Should().Be(ResultStatus.NotFound);

        countryRegionRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
    }

    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_error_given_exception_was_thrown(
        [Frozen] Mock<IRepository<Entities.CountryRegion>> countryRegionRepoMock,
        GetCountriesQueryHandler sut,
        GetCountriesQuery query
    )
    {
        //Arrange
        countryRegionRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception());

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.Status.Should().Be(ResultStatus.Error);

        countryRegionRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
    }
}
