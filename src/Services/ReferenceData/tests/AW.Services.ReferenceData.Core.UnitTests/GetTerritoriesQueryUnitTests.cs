using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Handlers.Territory.GetTerritories;
using AW.Services.ReferenceData.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.ReferenceData.Core.UnitTests;

public class GetTerritoriesQueryUnitTests
{
    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_success_given_territories_exist_with_no_filter(
        List<Entities.Territory> territories,
        [Frozen] Mock<IRepository<Entities.Territory>> territoryRepoMock,
        GetTerritoriesQueryHandler sut,
        GetTerritoriesQuery query
    )
    {
        //Arrange
        territories = territories.OrderBy(x => x.Name).ToList();

        territoryRepoMock.Setup(x => x.ListAsync(
                It.IsAny<CancellationToken>()
            )
        )
        .ReturnsAsync(territories);

        //Act
        query.CountryRegionCode = null;
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(territories, opt => opt
            .Excluding(_ => _.Id)
        );

        territoryRepoMock.Verify(x => x.ListAsync(
                It.IsAny<CancellationToken>()
            )
        );
    }

    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_success_given_territories_exist_with_filter(
        List<Entities.Territory> territories,
        [Frozen] Mock<IRepository<Entities.Territory>> territoryRepoMock,
        GetTerritoriesQueryHandler sut,
        GetTerritoriesQuery query
    )
    {
        //Arrange
        territories = territories.OrderBy( x => x.Name ).ToList();

        territoryRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetTerritoriesForCountrySpecification>(),
                It.IsAny<CancellationToken>()
            )
        )
        .ReturnsAsync(territories);

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(territories, opt => opt
            .Excluding(_ => _.Id)
        );

        territoryRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetTerritoriesForCountrySpecification>(),
                It.IsAny<CancellationToken>()
            )
        );
    }

    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_notfound_given_no_territories_exist(
        [Frozen] Mock<IRepository<Entities.Territory>> territoryRepoMock,
        GetTerritoriesQueryHandler sut,
        GetTerritoriesQuery query
    )
    {
        //Arrange
        query.CountryRegionCode = null;

        territoryRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Entities.Territory>());

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.Status.Should().Be(ResultStatus.NotFound);

        territoryRepoMock.Verify(x => x.ListAsync(
                It.IsAny<CancellationToken>()
            )
        );
    }

    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_error_given_exception_was_thrown(
        [Frozen] Mock<IRepository<Entities.Territory>> territoryRepoMock,
        GetTerritoriesQueryHandler sut,
        GetTerritoriesQuery query
    )
    {
        //Arrange
        query.CountryRegionCode = null;

        territoryRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception());

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.Status.Should().Be(ResultStatus.Error);

        territoryRepoMock.Verify(x => x.ListAsync(
                It.IsAny<CancellationToken>()
            )
        );
    }
}
