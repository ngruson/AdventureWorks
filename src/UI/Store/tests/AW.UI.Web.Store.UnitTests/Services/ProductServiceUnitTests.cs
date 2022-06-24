using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients.ProductApi;
using AW.UI.Web.Infrastructure.ApiClients.ProductApi.Models;
using AW.UI.Web.Store.Services;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Store.UnitTests.Services
{
    public class ProductServiceUnitTests
    {
        public class GetCategories
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetCategories_CategoriesExist_ReturnsCategories(
                [Frozen] Mock<IProductApiClient> mockClient,
                ProductService sut,
                List<ProductCategory> categories
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetCategoriesAsync())
                    .ReturnsAsync(categories);

                //Act
                var response = await sut.GetCategoriesAsync();

                //Assert
                response.Should().BeEquivalentTo(categories);
            }
        }

        public class GetProducts
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetCategories_CategoriesExist_ReturnsCategories(
                [Frozen] Mock<IProductApiClient> mockClient,
                ProductService sut,
                GetProductsResult products,
                int pageIndex,
                int pageSize,
                string category,
                string subcategory
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetProductsAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                ))
                .ReturnsAsync(products);

                //Act
                var response = await sut.GetProductsAsync(
                    pageIndex,
                    pageSize,
                    category,
                    subcategory
                );

                //Assert
                response.Should().BeEquivalentTo(products);
            }
        }
    }
}