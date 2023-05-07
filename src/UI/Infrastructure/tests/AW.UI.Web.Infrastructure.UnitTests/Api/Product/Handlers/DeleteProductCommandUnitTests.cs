using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.DeleteProduct;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Product.Handlers
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
