using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductModels;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Product.Handlers
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
            mockProductApiClient.Setup(_ => _.GetProductModels())
                .ReturnsAsync((List<ProductModel>?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockProductApiClient.Verify(x => x.GetProductModels());
        }
    }
}
