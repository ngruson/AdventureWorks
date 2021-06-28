using Ardalis.Specification;
using AW.Services.Product.Application.CountProducts;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Services.Product.Application.UnitTests
{
    public class CountProductsQueryUnitTests
    {
        [Fact]
        public async void Handle_ProductExists_ReturnProduct()
        {
            // Arrange
            var product = new TestBuilders.ProductBuilder().WithTestValues().Build();

            var loggerMock = new Mock<ILogger<CountProductsQueryHandler>>();
            var productRepoMock = new Mock<IRepositoryBase<Domain.Product>>();
            productRepoMock.Setup(x => x.CountAsync(It.IsAny<ISpecification<Domain.Product>>()))
                .ReturnsAsync(10);

            var handler = new CountProductsQueryHandler(
                loggerMock.Object,
                productRepoMock.Object
            );

            //Act
            var query = new CountProductsQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(10);
            productRepoMock.Verify(x => x.CountAsync(It.IsAny<ISpecification<Domain.Product>>()));
        }
    }
}