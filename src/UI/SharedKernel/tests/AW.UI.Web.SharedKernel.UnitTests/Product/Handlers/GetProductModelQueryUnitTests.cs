using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductModel;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Product.Handlers
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
