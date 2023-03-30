using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Product.Handlers.DuplicateProduct;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Product.Handlers
{
    public class DuplicateProductCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task DuplicateProductGivenCommandIsValid(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            DuplicateProductCommandHandler sut,
            DuplicateProductCommand command,
            SharedKernel.Product.Handlers.DuplicateProduct.Product product
        )
        {
            //Arrange
            mockProductApiClient.Setup(_ => _.DuplicateProduct(
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync(product);

            //Act
            var duplicatedProduct = await sut.Handle(command, CancellationToken.None);

            //Assert
            duplicatedProduct.Should().Be(product);

            mockProductApiClient.Verify(_ => _.DuplicateProduct(
                It.IsAny<string>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task ThrowArgumentExceptionGivenCommandIsInvalid(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            DuplicateProductCommandHandler sut
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(
                new DuplicateProductCommand(null!), CancellationToken.None
            );

            //Assert
            await func.Should().ThrowAsync<ArgumentException>();

            mockProductApiClient.Verify(_ => _.DuplicateProduct(
                    It.IsAny<string>()
                )
                , Times.Never
            );
        }
    }
}
