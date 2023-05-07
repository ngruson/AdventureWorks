using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.SalesOrder.Handlers
{
    public class GetSalesOrderQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_WithSalesOrderNumber_SalesOrderReturned(
            [Frozen] Mock<ISalesOrderApiClient> mockSalesOrderApiClient,
            GetSalesOrderQueryHandler sut,
            GetSalesOrderQuery query,
            Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.SalesOrder salesOrder
        )
        {
            //Arrange
            mockSalesOrderApiClient.Setup(_ => _.GetSalesOrderAsync(
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync(salesOrder);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(salesOrder);

            mockSalesOrderApiClient.Verify(x => x.GetSalesOrderAsync(
                    It.IsAny<string>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_WithoutSalesOrderNumber_ThrowsArgumentException(
            [Frozen] Mock<ISalesOrderApiClient> mockSalesOrderApiClient,
            GetSalesOrderQueryHandler sut,
            GetSalesOrderQuery query
        )
        {
            //Arrange
            query.SalesOrderNumber = "";

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Required input request.SalesOrderNumber was empty. (Parameter 'request.SalesOrderNumber')");

            mockSalesOrderApiClient.Verify(x => x.GetSalesOrderAsync(
                    It.IsAny<string>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_ReturnedSalesOrderNull_ThrowsArgumentNullException(
            [Frozen] Mock<ISalesOrderApiClient> mockSalesOrderApiClient,
            GetSalesOrderQueryHandler sut,
            GetSalesOrderQuery query
        )
        {
            //Arrange
            mockSalesOrderApiClient.Setup(_ => _.GetSalesOrderAsync(
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync((Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.SalesOrder?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'salesOrder')");

            mockSalesOrderApiClient.Verify(x => x.GetSalesOrderAsync(
                    It.IsAny<string>()
                )
            );
        }
    }
}
