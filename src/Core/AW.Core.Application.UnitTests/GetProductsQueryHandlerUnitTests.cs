using Ardalis.Specification;
using AW.Core.Application.Product.GetProducts;
using AW.Core.Application.Specifications;
using AW.Core.Application.UnitTests.AutoMapper;
using AW.Core.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Core.Application.UnitTests
{
    public class GetProductsQueryHandlerUnitTests
    {
        [Fact]
        public async void Handle_ProductsExists_ReturnProducts()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var product = new ProductBuilder().WithTestValues().Build();

            var productRepoMock = new Mock<IRepositoryBase<Domain.Production.Product>>();
            productRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetProductsPaginatedSpecification>()))
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