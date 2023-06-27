using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.ReferenceData.Core.UnitTests;

public class GetAddressTypesQueryUnitTests
{
    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_success_given_address_types_exists(
        List<Entities.AddressType> addressTypes,
        [Frozen] Mock<IRepository<Entities.AddressType>> addressTypeRepoMock,
        GetAddressTypesQueryHandler sut,
        GetAddressTypesQuery query
    )
    {
        //Arrange
        addressTypeRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(addressTypes);

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(addressTypes, opt => opt
            .Excluding(_ => _.Id)
        );

        addressTypeRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
    }

    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_notfound_given_no_addresstypes_exist(
        [Frozen] Mock<IRepository<Entities.AddressType>> addressTypeRepoMock,
        GetAddressTypesQueryHandler sut,
        GetAddressTypesQuery query
    )
    {
        //Arrange
        addressTypeRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Entities.AddressType>());

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.Status.Should().Be(ResultStatus.NotFound);

        addressTypeRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
    }

    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_error_given_exception_was_thrown(
        [Frozen] Mock<IRepository<Entities.AddressType>> addressTypeRepoMock,
        GetAddressTypesQueryHandler sut,
        GetAddressTypesQuery query
    )
    {
        //Arrange
        addressTypeRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception());

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.Status.Should().Be(ResultStatus.Error);

        addressTypeRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
    }
}
