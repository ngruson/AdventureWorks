using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProducts;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Product.Handlers
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
            mockProductApiClient.Setup(_ => _.GetProducts(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            ))
            .ReturnsAsync((GetProductsResult?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockProductApiClient.Verify(_ => _.GetProducts(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            ));
        }
    }
}
