using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductModels;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Product.Handlers
{
    public class GetProductModelsQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnProductModelsGivenProductModelsAreCached(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            GetProductModelsQueryHandler sut,
            GetProductModelsQuery query,
            List<ProductModel> productModels
        )
        {
            //Arrange
            mockProductApiClient.Setup(_ => _.GetProductModels())
                .ReturnsAsync(productModels);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(productModels);

            mockProductApiClient.Verify(x => x.GetProductModels());
        }

        [Theory, AutoMoqData]
        public async Task ThrowArgumentNullExceptionGivenProductModelsAreNull(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            GetProductModelsQueryHandler sut,
            GetProductModelsQuery query
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockProductApiClient.Verify(x => x.GetProductModels());
        }
    }
}
