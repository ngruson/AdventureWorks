using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Handlers.ShipMethod.GetShipMethods;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.ReferenceData.Core.UnitTests;

public class GetShipMethodsQueryUnitTests
{
    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_success_given_shipmethods_exist(
        List<Entities.ShipMethod> shipMethods,
        [Frozen] Mock<IRepository<Entities.ShipMethod>> shipMethodRepoMock,
        GetShipMethodsQueryHandler sut,
        GetShipMethodsQuery query
    )
    {
        //Arrange
        shipMethods = shipMethods.OrderBy(_ => _.Name).ToList();

        shipMethodRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(shipMethods);

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(shipMethods, opt => opt
            .Excluding(_ => _.Id)
        );

        shipMethodRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
    }

    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_notfound_given_no_shipmethods_exist(
        [Frozen] Mock<IRepository<Entities.ShipMethod>> shipMethodRepoMock,
        GetShipMethodsQueryHandler sut,
        GetShipMethodsQuery query
    )
    {
        //Arrange
        shipMethodRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Entities.ShipMethod>());

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.Status.Should().Be(ResultStatus.NotFound);

        shipMethodRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
    }

    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_error_given_exception_was_thrown(
        [Frozen] Mock<IRepository<Entities.ShipMethod>> shipMethodRepoMock,
        GetShipMethodsQueryHandler sut,
        GetShipMethodsQuery query
    )
    {
        //Arrange
        shipMethodRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception());

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.Status.Should().Be(ResultStatus.Error);

        shipMethodRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
    }
}
