using AutoFixture.Xunit2;
using AW.SharedKernel.Caching;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Product.Handlers
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

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().BeNull();

            mockCache.Verify(x => x.GetData());
        }
    }
}
