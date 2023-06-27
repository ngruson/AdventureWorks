using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.ReferenceData.Core.UnitTests;

public class GetContactTypesQueryUnitTests
{
    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_success_given_contacttypes_exist(
        List<Entities.ContactType> contactTypes,
        [Frozen] Mock<IRepository<Entities.ContactType>> contactTypeRepoMock,
        GetContactTypesQueryHandler sut,
        GetContactTypesQuery query
    )
    {
        //Arrange            
        contactTypeRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(contactTypes);

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(contactTypes, opt => opt
            .Excluding(_ => _.Id)
        );

        contactTypeRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
    }

    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_notfound_given_no_contacttypes_exist(
        [Frozen] Mock<IRepository<Entities.ContactType>> contactTypeRepoMock,
        GetContactTypesQueryHandler sut,
        GetContactTypesQuery query
    )
    {
        //Arrange
        contactTypeRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Entities.ContactType>());

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.Status.Should().Be(ResultStatus.NotFound);

        contactTypeRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
    }

    [Theory, AutoMapperData(typeof(MappingProfile))]
    public async Task return_error_given_exception_was_thrown(
        [Frozen] Mock<IRepository<Entities.ContactType>> contactTypeRepoMock,
        GetContactTypesQueryHandler sut,
        GetContactTypesQuery query
    )
    {
        //Arrange
        contactTypeRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception());

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        result.Status.Should().Be(ResultStatus.Error);

        contactTypeRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
    }
}
