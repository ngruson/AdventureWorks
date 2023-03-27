using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.SalesOrder.Handlers.UpdateSalesOrder;
using FluentAssertions;
using MediatR;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.SalesOrder.Handlers
{
    public class UpdateSalesOrderCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_WithSalesOrder_SalesOrderUpdated(
            [Frozen] Mock<ISalesOrderApiClient> mockSalesOrderApiClient,
            UpdateSalesOrderCommandHandler sut,
            UpdateSalesOrderCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);
            
            //Assert
            mockSalesOrderApiClient.Verify(x => x.UpdateSalesOrderAsync(
                    It.IsAny<SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.SalesOrder>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_WithoutSalesOrder_ThrowsArgumentNullException(
            [Frozen] Mock<ISalesOrderApiClient> mockSalesOrderApiClient,
            UpdateSalesOrderCommandHandler sut
        )
        {
            //Arrange
            var command = new UpdateSalesOrderCommand(null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'request.SalesOrder')");

            mockSalesOrderApiClient.Verify(x => x.UpdateSalesOrderAsync(
                    It.IsAny<SharedKernel.SalesOrder.Handlers.UpdateSalesOrder.SalesOrder>()
                ),
                Times.Never
            );
        }
    }
}
