using Ardalis.Specification;
using AW.Application.Product.GetProduct;
using AW.Application.Specifications;
using AW.Application.UnitTests.AutoMapper;
using AW.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
{
    public class GetProductQueryHandlerUnitTests
    {
        [Fact]
        public async void Handle_ProductExists_ReturnProduct()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var product = new ProductBuilder().WithTestValues().Build();

            var productRepoMock = new Mock<IRepositoryBase<Domain.Production.Product>>();
            productRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetProductSpecification>()))
                .ReturnsAsync(product);

            var handler = new GetProductQueryHandler(
                productRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetProductQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
        }
    }
}