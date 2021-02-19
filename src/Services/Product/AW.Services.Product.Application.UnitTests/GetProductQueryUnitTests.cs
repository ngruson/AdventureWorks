using Ardalis.Specification;
using AW.Services.Product.Application.GetProduct;
using AW.Services.Product.Application.Specifications;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Product.Application.UnitTests
{
    public class GetProductQueryUnitTests
    {
        [Fact]
        public async void Handle_ProductExists_ReturnProduct()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var product = new TestBuilders.ProductBuilder().WithTestValues().Build();

            var loggerMock = new Mock<ILogger<GetProductQueryHandler>>();
            var productRepoMock = new Mock<IRepositoryBase<Domain.Product>>();
            productRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetProductSpecification>()))
                .ReturnsAsync(product);

            var handler = new GetProductQueryHandler(
                loggerMock.Object,
                productRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetProductQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            productRepoMock.Verify(x => x.GetBySpecAsync(It.IsAny<ISpecification<Domain.Product>>()));
            result.ProductNumber.Should().Be("FR-R92B-58");
        }

        [Fact]
        public void Handle_ProductDoesNotExists_ThrowArgumentNullException()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();

            var loggerMock = new Mock<ILogger<GetProductQueryHandler>>();
            var productRepoMock = new Mock<IRepositoryBase<Domain.Product>>();

            var handler = new GetProductQueryHandler(
                loggerMock.Object,
                productRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetProductQuery();
            Func<Task> func = async() => await handler.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>();
            productRepoMock.Verify(x => x.GetBySpecAsync(It.IsAny<ISpecification<Domain.Product>>()));
        }
    }
}