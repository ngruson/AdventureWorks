using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Product.Handlers.UpdateProduct;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Product.Handlers
{
    public class UpdateProductCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task UpdateProductGivenCommandIsValid(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            UpdateProductCommandHandler sut,
            UpdateProductCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockProductApiClient.Verify(_ => _.UpdateProduct(
                It.IsAny<string>(),
                It.IsAny<SharedKernel.Product.Handlers.UpdateProduct.Product>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task ThrowArgumentNullExceptionGivenCommandIsInvalid(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            UpdateProductCommandHandler sut
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(
                new UpdateProductCommand(null!, null!), CancellationToken.None
            );

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockProductApiClient.Verify(_ => _.UpdateProduct(
                    It.IsAny<string>(),
                    It.IsAny<SharedKernel.Product.Handlers.UpdateProduct.Product>()
                )
                , Times.Never
            );
        }
    }
}
