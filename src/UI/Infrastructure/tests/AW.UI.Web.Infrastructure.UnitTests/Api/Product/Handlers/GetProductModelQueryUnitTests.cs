using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductModel;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Product.Handlers
{
    public class GetProductModelQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnProductModelGivenProductModelExists(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            GetProductModelQueryHandler sut,
            GetProductModelQuery query,
            ProductModel productModel
        )
        {
            //Arrange
            mockProductApiClient.Setup(_ => _.GetProductModel(
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync(productModel);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(productModel);

            mockProductApiClient.Verify(_ =>
                _.GetProductModel(It.IsAny<string>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task ThrowArgumentNullExceptionGivenProductModelIsNull(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            GetProductModelQueryHandler sut,
            GetProductModelQuery query
        )
        {
            //Arrange
            mockProductApiClient.Setup(_ => _.GetProductModel(
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync((ProductModel?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockProductApiClient.Verify(x => x.GetProductModel(
                It.IsAny<string>()
            ));
        }
    }
}
