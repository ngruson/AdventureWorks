using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProduct;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Product.Handlers
{
    public class GetProductQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnProductGivenProductExists(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            GetProductQueryHandler sut,
            GetProductQuery query,
            Infrastructure.Api.Product.Handlers.GetProduct.Product product
        )
        {
            //Arrange
            mockProductApiClient.Setup(_ => _.GetProduct(
                It.IsAny<string>()
            ))
            .ReturnsAsync(product);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(product);

            mockProductApiClient.Verify(x => x.GetProduct(
                It.IsAny<string>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task ThrowArgumentNullExceptionGivenProductIsNull(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            GetProductQueryHandler sut,
            GetProductQuery query
        )
        {
            //Arrange
            mockProductApiClient.Setup(_ => _.GetProduct(
                It.IsAny<string>()
            ))
            .ReturnsAsync((Infrastructure.Api.Product.Handlers.GetProduct.Product?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockProductApiClient.Verify(x => x.GetProduct(It.IsAny<string>()));
        }
    }
}
