using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.ApproveSalesOrder;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.SalesOrder.Handlers
{
    public class ApproveSalesOrderCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_WithSalesOrderNumber_SalesOrderApproved(
            [Frozen] Mock<ISalesOrderApiClient> mockSalesOrderApiClient,
            ApproveSalesOrderCommandHandler sut,
            ApproveSalesOrderCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockSalesOrderApiClient.Verify(x => x.ApproveSalesOrderAsync(
                    It.IsAny<string>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_WithoutSalesOrderNumber_ThrowsArgumentException(
            [Frozen] Mock<ISalesOrderApiClient> mockSalesOrderApiClient,
            ApproveSalesOrderCommandHandler sut
        )
        {
            //Arrange
            var command = new ApproveSalesOrderCommand(null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Required input request.SalesOrderNumber was empty. (Parameter 'request.SalesOrderNumber')");

            mockSalesOrderApiClient.Verify(x => x.ApproveSalesOrderAsync(
                    It.IsAny<string>()
                ),
                Times.Never
            );
        }
    }
}
