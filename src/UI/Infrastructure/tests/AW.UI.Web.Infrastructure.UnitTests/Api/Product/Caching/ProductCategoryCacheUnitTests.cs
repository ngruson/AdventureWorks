using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Product.Caching;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Product.Caching
{
    public class ProductCategoryCacheUnitTests
    {
        public class GetData
        {
            [Theory, AutoMoqData]
            public async Task GetData_CacheNotSet_CategoriesAddedToCache(
                [Frozen] Mock<IMemoryCache> cacheMock,
                [Frozen] Mock<IProductApiClient> mockClient,
                ProductCategoryCache sut,
                List<Infrastructure.Api.Product.Handlers.GetProductCategories.ProductCategory> categories
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetCategories())
                    .ReturnsAsync(categories);

                //Act
                var result = await sut.GetData();

                //Assert
                result.Should().BeEquivalentTo(categories);
                cacheMock.Verify(_ => _.CreateEntry(
                        It.IsAny<string>()
                    )
                );
            }

            [Theory, AutoMoqData]
            public async Task GetData_CategoriesAreCached_CacheIsNotSet(
                [Frozen] Mock<IMemoryCache> cacheMock,
                ProductCategoryCache sut,
                List<Infrastructure.Api.Product.Handlers.GetProductCategories.ProductCategory> categories
            )
            {
                //Arrange
                object value = categories;
                cacheMock.Setup(_ => _.TryGetValue(
                        It.IsAny<object>(),
                        out value!
                    )
                )
                .Returns(true);

                //Act
                var result = await sut.GetData();

                //Assert
                result.Should().BeEquivalentTo(categories);
                cacheMock.Verify(_ => _.CreateEntry(
                        It.IsAny<string>()
                    ),
                    Times.Never
                );
            }
        }

        public class GetDataWithPredicate
        {
            [Theory, AutoMoqData]
            public async Task GetData_FilteredCategories(
                [Frozen] Mock<IProductApiClient> mockClient,
                ProductCategoryCache sut,
                List<Infrastructure.Api.Product.Handlers.GetProductCategories.ProductCategory> categories
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetCategories())
                    .ReturnsAsync(categories);

                //Act
                var result = await sut.GetData(_ => _.Name == categories[0].Name);

                //Assert
                result.Should().BeEquivalentTo(new[] { categories[0] });
            }
        }
    }
}
