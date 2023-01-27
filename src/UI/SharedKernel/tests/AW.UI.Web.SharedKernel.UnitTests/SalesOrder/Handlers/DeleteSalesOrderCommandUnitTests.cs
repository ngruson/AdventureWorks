using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.SalesOrder.Handlers.DeleteSalesOrder;
using FluentAssertions;
using MediatR;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.SalesOrder.Handlers
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
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().Be(Unit.Value);
            
            mockSalesOrderApiClient.Verify(x => x.ApproveSalesOrderAsync(
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