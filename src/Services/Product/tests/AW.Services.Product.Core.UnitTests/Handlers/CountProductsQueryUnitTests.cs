using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.Product.Core.Handlers.CountProducts;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Product.Core.UnitTests.Handlers
{
    public class CountProductsQueryUnitTests
    {
        [Theory, AutoMoqData()]
        public async Task Handle_ProductExists_ReturnProduct(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            CountProductsQueryHandler sut,
            CountProductsQuery query,
            [Frozen] int count
        )
        {
            // Arrange
            productRepoMock.Setup(x => x.CountAsync(
                It.IsAny<ISpecification<Entities.Product>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(count);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(count);
            productRepoMock.Verify(x => x.CountAsync(
                It.IsAny<ISpecification<Entities.Product>>(),
                It.IsAny<CancellationToken>()
            ));
        }
    }
}