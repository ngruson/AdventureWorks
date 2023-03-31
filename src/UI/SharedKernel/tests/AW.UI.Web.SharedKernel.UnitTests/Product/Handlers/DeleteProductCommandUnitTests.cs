using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Product.Handlers.DeleteProduct;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Product.Handlers
{
    public class DeleteProductCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task DeleteProductGivenCommandIsValid(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            DeleteProductCommandHandler sut,
            DeleteProductCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockProductApiClient.Verify(_ => _.DeleteProduct(
                It.IsAny<string>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task ThrowArgumentExceptionGivenCommandIsInvalid(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            DeleteProductCommandHandler sut
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(
                new DeleteProductCommand(null!), CancellationToken.None
            );

            //Assert
            await func.Should().ThrowAsync<ArgumentException>();

            mockProductApiClient.Verify(_ => _.DeleteProduct(
                    It.IsAny<string>()
                )
                , Times.Never
            );
        }
    }
}
