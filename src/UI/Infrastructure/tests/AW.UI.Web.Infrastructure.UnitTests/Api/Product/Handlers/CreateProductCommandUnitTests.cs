using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.CreateProduct;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Product.Handlers
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
                It.IsAny<Infrastructure.Api.Product.Handlers.CreateProduct.Product>()
            ));
        }
    }
}
