using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.UpdateProduct;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Product.Handlers
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
                It.IsAny<Infrastructure.Api.Product.Handlers.UpdateProduct.Product>()
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
                    It.IsAny<Infrastructure.Api.Product.Handlers.UpdateProduct.Product>()
                )
                , Times.Never
            );
        }
    }
}
