using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProducts;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Product.Handlers
{
    public class GetProductsQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnProductsGivenProductsExist(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            GetProductsQueryHandler sut,
            GetProductsQuery query,
            GetProductsResult products
        )
        {
            //Arrange
            mockProductApiClient.Setup(_ => _.GetProducts(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            ))
            .ReturnsAsync(products);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(products);

            mockProductApiClient.Verify(_ => _.GetProducts(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task ThrowArgumentNullExceptionGivenProductsAreNull(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            GetProductsQueryHandler sut,
            GetProductsQuery query
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockProductApiClient.Verify(_ => _.GetProducts(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            ));
        }
    }
}
