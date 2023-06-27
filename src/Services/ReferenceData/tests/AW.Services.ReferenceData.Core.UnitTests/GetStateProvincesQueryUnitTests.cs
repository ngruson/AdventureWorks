using AW.Services.ReferenceData.Core.Specifications;
using AW.Services.ReferenceData.Core.Handlers.StateProvince.GetStatesProvinces;
using AW.Services.SharedKernel.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;
using AW.SharedKernel.UnitTesting;
using AutoFixture.Xunit2;
using Ardalis.Result;

namespace AW.Services.ReferenceData.Core.UnitTests;

public class GetStateProvincesQueryUnitTests
{
    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_success_given_stateprovinces_exist_with_no_filter(
        List<Entities.StateProvince> statesProvinces,
        [Frozen] Mock<IRepository<Entities.StateProvince>> stateProvinceRepoMock,
        GetStatesProvincesQueryHandler sut,
        GetStatesProvincesQuery query
    )
    {
        //Arrange
        stateProvinceRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetStatesProvincesSpecification>(),
                It.IsAny<CancellationToken>()
            )
        )
        .ReturnsAsync(statesProvinces);

        query.CountryRegionCode = "";

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(statesProvinces, opt => opt
            .Excluding(_ => _.Id)
            .Excluding(_ => _.CountryRegion)
        );

        stateProvinceRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetStatesProvincesSpecification>(),
                It.IsAny<CancellationToken>()
            )
        );
    }

    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_success_given_stateprovinces_exist_with_filter(
        List<Entities.StateProvince> statesProvinces,
        [Frozen] Mock<IRepository<Entities.StateProvince>> stateProvinceRepoMock,
        GetStatesProvincesQueryHandler sut,
        GetStatesProvincesQuery query
    )
    {
        //Arrange
        stateProvinceRepoMock.Setup(x => x.ListAsync(
            It.IsAny<GetStatesProvincesSpecification>(),
            It.IsAny<CancellationToken>()
        ))
        .ReturnsAsync(statesProvinces);

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(statesProvinces, opt => opt
            .Excluding(_ => _.Id)
            .Excluding(_ => _.CountryRegion)
        );

        stateProvinceRepoMock.Verify(x => x.ListAsync(
            It.IsAny<GetStatesProvincesSpecification>(),
            It.IsAny<CancellationToken>()
        ));
    }

    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_notfound_given_no_statesprovinces_exist(
        [Frozen] Mock<IRepository<Entities.StateProvince>> stateProvinceRepoMock,
        GetStatesProvincesQueryHandler sut,
        GetStatesProvincesQuery query
    )
    {
        //Arrange
        stateProvinceRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetStatesProvincesSpecification>(),
                It.IsAny<CancellationToken>()
            )
        )
        .ReturnsAsync(new List<Entities.StateProvince>());

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.Status.Should().Be(ResultStatus.NotFound);

        stateProvinceRepoMock.Verify(x => x.ListAsync(
            It.IsAny<GetStatesProvincesSpecification>(),
            It.IsAny<CancellationToken>()
            )
        );
    }

    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_error_given_exception_was_thrown(
        [Frozen] Mock<IRepository<Entities.StateProvince>> stateProvinceRepoMock,
        GetStatesProvincesQueryHandler sut,
        GetStatesProvincesQuery query
    )
    {
        //Arrange
        stateProvinceRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetStatesProvincesSpecification>(),
                It.IsAny<CancellationToken>()
            )
        )
        .ThrowsAsync(new Exception());

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.Status.Should().Be(ResultStatus.Error);

        stateProvinceRepoMock.Verify(x => x.ListAsync(
            It.IsAny<GetStatesProvincesSpecification>(),
            It.IsAny<CancellationToken>()
            )
        );
    }
}
