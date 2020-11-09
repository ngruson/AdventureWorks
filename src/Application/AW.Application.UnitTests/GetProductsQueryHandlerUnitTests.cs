using AW.Application.Interfaces;
using AW.Application.Product.GetProducts;
using AW.Application.Specifications;
using AW.Application.UnitTests.AutoMapper;
using AW.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
{
    public class GetProductsQueryHandlerUnitTests
    {
        [Fact]
        public async void Handle_ProductsExists_ReturnProducts()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var product = new ProductBuilder().WithTestValues().Build();

            var productRepoMock = new Mock<IAsyncRepository<Domain.Production.Product>>();
            productRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetProductsPaginatedSpecification>()))
                .ReturnsAsync(product);

            var handler = new GetProductsQueryHandler(
                productRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetProductsQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
        }
    }
}