using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.Product.Core.AutoMapper;
using AW.Services.Product.Core.Exceptions;
using AW.Services.Product.Core.Handlers.GetLocations;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Product.Core.UnitTests.Handlers
{
    public class GetLocationsQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ReturnLocationsGivenLocationsExist(
            List<Entities.Location> locations,
            [Frozen] Mock<IRepository<Entities.Location>> repoMock,
            GetLocationsQueryHandler sut,
            GetLocationsQuery query
        )
        {
            // Arrange
            repoMock.Setup(x => x.ListAsync(
                It.IsAny<GetLocationsSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(locations);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            repoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.Location>>(),
                It.IsAny<CancellationToken>()
            ));

            result.Should().BeEquivalentTo(locations, options => options
                .Excluding(_ => _.Id)
                .Excluding(_ => _.ProductInventory)
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ThrowLocationsNotFoundExceptionGivenNoLocationsExist(
            [Frozen] Mock<IRepository<Entities.Location>> repoMock,
            GetLocationsQueryHandler sut,
            GetLocationsQuery query
        )
        {
            // Arrange
            repoMock.Setup(x => x.ListAsync(
                It.IsAny<GetLocationsSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new List<Entities.Location>());

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<LocationsNotFoundException>()
                .WithMessage("Locations not found");
            repoMock.Verify(x => x.ListAsync(
                It.IsAny<GetLocationsSpecification>(),
                It.IsAny<CancellationToken>()
            ));
        }
    }
}
