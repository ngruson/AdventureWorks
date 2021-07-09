using AW.Services.Product.Core.Handlers.GetProduct;
using AW.Services.Product.Core.Specifications;
using AW.SharedKernel.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Product.Core.UnitTests
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
            var productRepoMock = new Mock<IRepository<Entities.Product>>();
            productRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetProductSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(product);

            var handler = new GetProductQueryHandler(
                loggerMock.Object,
                productRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetProductQuery { ProductNumber = "FR-R92B-58" };
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            productRepoMock.Verify(x => x.GetBySpecAsync(
                It.IsAny<GetProductSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            result.ProductNumber.Should().Be("FR-R92B-58");
        }

        [Fact]
        public void Handle_ProductDoesNotExists_ThrowArgumentNullException()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();

            var loggerMock = new Mock<ILogger<GetProductQueryHandler>>();
            var productRepoMock = new Mock<IRepository<Entities.Product>>();

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
            productRepoMock.Verify(x => x.GetBySpecAsync(
                It.IsAny<GetProductSpecification>(),
                It.IsAny<CancellationToken>()
            ));
        }
    }
}