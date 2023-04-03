using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Product.Handlers.CreateProduct;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Product.Handlers
{
    public class CreateProductCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnsProductGivenProductIsAdded(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            CreateProductCommandHandler sut,
            CreateProductCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockProductApiClient.Verify(_ => _.CreateProduct(
                It.IsAny<SharedKernel.Product.Handlers.CreateProduct.Product>()
            ));
        }
    }
}
