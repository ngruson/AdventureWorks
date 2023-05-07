using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.UpdateSalesOrder;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.SalesOrder.Handlers
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
                    It.IsAny<Infrastructure.Api.SalesOrder.Handlers.UpdateSalesOrder.SalesOrder>()
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
                    It.IsAny<Infrastructure.Api.SalesOrder.Handlers.UpdateSalesOrder.SalesOrder>()
                ),
                Times.Never
            );
        }
    }
}
