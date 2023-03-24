using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProduct;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Product.Handlers
{
    public class GetProductQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnProductGivenProductExists(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            GetProductQueryHandler sut,
            GetProductQuery query,
            SharedKernel.Product.Handlers.GetProduct.Product product
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

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockProductApiClient.Verify(x => x.GetProduct(It.IsAny<string>()));
        }
    }
}
