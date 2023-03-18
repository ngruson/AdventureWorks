using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Product.Caching;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AW.UI.Web.SharedKernel.UnitTests.Product.Caching
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
                List<SharedKernel.Product.Handlers.GetProductCategories.ProductCategory> categories
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
                List<SharedKernel.Product.Handlers.GetProductCategories.ProductCategory> categories
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
                List<SharedKernel.Product.Handlers.GetProductCategories.ProductCategory> categories
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
