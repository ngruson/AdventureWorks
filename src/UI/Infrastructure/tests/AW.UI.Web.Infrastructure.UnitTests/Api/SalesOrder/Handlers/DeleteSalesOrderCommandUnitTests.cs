using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.DeleteSalesOrder;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.SalesOrder.Handlers
{
    public class DeleteSalesOrderCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_WithSalesOrderNumber_SalesOrderDeleted(
            [Frozen] Mock<ISalesOrderApiClient> mockSalesOrderApiClient,
            DeleteSalesOrderCommandHandler sut,
            DeleteSalesOrderCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockSalesOrderApiClient.Verify(x => x.DeleteSalesOrderAsync(
                    It.IsAny<string>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_WithoutSalesOrderNumber_ThrowsArgumentException(
            [Frozen] Mock<ISalesOrderApiClient> mockSalesOrderApiClient,
            DeleteSalesOrderCommandHandler sut
        )
        {
            //Arrange
            var command = new DeleteSalesOrderCommand(null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Required input request.SalesOrderNumber was empty. (Parameter 'request.SalesOrderNumber')");

            mockSalesOrderApiClient.Verify(x => x.DuplicateSalesOrderAsync(
                    It.IsAny<string>()
                ),
                Times.Never
            );
        }
    }
}
