using Ardalis.Specification;
using AW.Services.Product.Core.Handlers.GetProductCategories;
using AW.Services.Product.Core.Specifications;
using AW.Services.Product.Core.UnitTests.TestBuilders;
using AW.SharedKernel.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Product.Core.UnitTests
{
    public class GetProductCategoriesQueryUnitTests
    {
        [Fact]
        public async void Handle_ProductCategoriesExists_ReturnProductCategories()
        {
            // Arrange
            var productCategories = new List<Entities.ProductCategory>
            {
                new ProductCategoryBuilder().Name("Bikes").Build(),
                new ProductCategoryBuilder().Name("Components").Build(),
                new ProductCategoryBuilder().Name("Clothing").Build(),
                new ProductCategoryBuilder().Name("Accessories").Build()
            };

            var loggerMock = new Mock<ILogger<GetProductCategoriesQueryHandler>>();
            var categoriesRepoMock = new Mock<IRepository<Entities.ProductCategory>>();
            categoriesRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetProductCategoriesSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(productCategories);

            var handler = new GetProductCategoriesQueryHandler(
                loggerMock.Object,
                categoriesRepoMock.Object,
                Mapper.CreateMapper()
            );

            //Act
            var query = new GetProductCategoriesQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            categoriesRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.ProductCategory>>(),
                It.IsAny<CancellationToken>()
            ));
            result[0].Name.Should().Be("Bikes");
            result[1].Name.Should().Be("Components");
            result[2].Name.Should().Be("Clothing");
            result[3].Name.Should().Be("Accessories");
        }

        [Fact]
        public void Handle_NoProductCategoriesExists_ThrowArgumentNullException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<GetProductCategoriesQueryHandler>>();
            var categoriesRepoMock = new Mock<IRepository<Entities.ProductCategory>>();

            var handler = new GetProductCategoriesQueryHandler(
                loggerMock.Object,
                categoriesRepoMock.Object,
                Mapper.CreateMapper()
            );

            //Act
            var query = new GetProductCategoriesQuery();
            Func<Task> func = async() => await handler.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>();
            categoriesRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.ProductCategory>>(),
                It.IsAny<CancellationToken>()
            ));
        }
    }
}