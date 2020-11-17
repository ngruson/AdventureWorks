using Ardalis.Specification;
using AW.Application.Product.CountProducts;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
{
    public class CountProductsQueryHandlerUnitTests
    {
        [Fact]
        public async void CountProducts_ProductsExist_ReturnProducts()
        {
            // Arrange
            var products = new List<Domain.Production.Product> {
                new Domain.Production.Product {  Name = "Product 1"},
                new Domain.Production.Product {  Name = "Product 2"},
                new Domain.Production.Product {  Name = "Product 3"}
            };

            var repoMock = new Mock<IRepositoryBase<Domain.Production.Product>>();
            repoMock.Setup(x => x.ListAsync(It.IsAny<ISpecification<Domain.Production.Product>>()))
                .ReturnsAsync(products);

            var handler = new CountProductsQueryHandler(repoMock.Object);

            //Act
            var result = await handler.Handle(new CountProductsQuery(), CancellationToken.None);

            //Assert
            result.Should().Be(3);
        }

        [Fact]
        public async void CountProducts_NoProductsExist_Return0()
        {
            // Arrange
            var products = new List<Domain.Production.Product>();

            var repoMock = new Mock<IRepositoryBase<Domain.Production.Product>>();
            repoMock.Setup(x => x.ListAsync(It.IsAny<ISpecification<Domain.Production.Product>>()))
                .ReturnsAsync(products);

            var handler = new CountProductsQueryHandler(repoMock.Object);

            //Act
            var result = await handler.Handle(new CountProductsQuery(), CancellationToken.None);

            //Assert
            result.Should().Be(0);
        }
    }
}