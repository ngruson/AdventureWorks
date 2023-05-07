using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.DuplicateProduct;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Product.Handlers
{
    public class DuplicateProductCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task DuplicateProductGivenCommandIsValid(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            DuplicateProductCommandHandler sut,
            DuplicateProductCommand command,
            Infrastructure.Api.Product.Handlers.DuplicateProduct.Product product
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
