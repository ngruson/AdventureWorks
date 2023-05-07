using AutoFixture.Xunit2;
using AW.SharedKernel.Caching;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductCategories;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Product.Handlers
{
    public class GetProductCategoriesQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnCategoriesGivenCategoriesAreCached(
            [Frozen] Mock<ICache<ProductCategory>> mockCache,
            GetProductCategoriesQueryHandler sut,
            GetProductCategoriesQuery query,
            List<ProductCategory> categories
        )
        {
            //Arrange
            mockCache.Setup(_ => _.GetData())
                .ReturnsAsync(categories);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(categories);

            mockCache.Verify(x => x.GetData());
        }

        [Theory, AutoMoqData]
        public async Task ReturnCategoriesGivenCategoriesAreNull(
            [Frozen] Mock<ICache<ProductCategory>> mockCache,
            GetProductCategoriesQueryHandler sut,
            GetProductCategoriesQuery query
        )
        {
            //Arrange
            mockCache.Setup(_ => _.GetData())
                .ReturnsAsync((List<ProductCategory>?)null);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().BeNull();

            mockCache.Verify(x => x.GetData());
        }
    }
}
