using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.Product.Core.AutoMapper;
using AW.Services.Product.Core.Exceptions;
using AW.Services.Product.Core.Handlers.GetUnitMeasures;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Product.Core.UnitTests.Handlers
{
    public class GetUnitMeasuresQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ReturnUnitMeasuresGivenUnitMeasuresExist(
            List<Entities.UnitMeasure> unitMeasures,
            [Frozen] Mock<IRepository<Entities.UnitMeasure>> repoMock,
            GetUnitMeasuresQueryHandler sut,
            GetUnitMeasuresQuery query
        )
        {
            // Arrange
            repoMock.Setup(x => x.ListAsync(
                It.IsAny<GetUnitMeasuresSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(unitMeasures);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            repoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.UnitMeasure>>(),
                It.IsAny<CancellationToken>()
            ));

            result.Should().BeEquivalentTo(unitMeasures);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ThrowUnitMeasuresNotFoundExceptionGivenNoUnitMeasuresExist(
            [Frozen] Mock<IRepository<Entities.UnitMeasure>> repoMock,
            GetUnitMeasuresQueryHandler sut,
            GetUnitMeasuresQuery query
        )
        {
            // Arrange
            repoMock.Setup(x => x.ListAsync(
                It.IsAny<GetUnitMeasuresSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new List<Entities.UnitMeasure>());

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<UnitMeasuresNotFoundException>()
                .WithMessage("Unit measures not found");
            repoMock.Verify(x => x.ListAsync(
                It.IsAny<GetUnitMeasuresSpecification>(),
                It.IsAny<CancellationToken>()
            ));
        }
    }
}
