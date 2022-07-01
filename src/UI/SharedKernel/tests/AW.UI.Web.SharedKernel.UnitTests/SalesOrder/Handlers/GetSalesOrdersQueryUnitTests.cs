using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrders;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.SalesOrder.Handlers
{
    public class GetSalesOrdersQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_ReturnedSalesOrdersNotNull_SalesOrdersReturned(
            [Frozen] Mock<ISalesOrderApiClient> mockSalesOrderApiClient,
            GetSalesOrdersQueryHandler sut,
            GetSalesOrdersQuery query,
            SalesOrdersResult salesOrdersResult
        )
        {
            //Arrange
            mockSalesOrderApiClient.Setup(_ => _.GetSalesOrdersAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string?>(),
                    It.IsAny<CustomerType>()
                )
            )
            .ReturnsAsync(salesOrdersResult);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(salesOrdersResult);

            mockSalesOrderApiClient.Verify(_ => _.GetSalesOrdersAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string?>(),
                    It.IsAny<CustomerType>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_ReturnedSalesOrderNull_ThrowsArgumentNullException(
            [Frozen] Mock<ISalesOrderApiClient> mockSalesOrderApiClient,
            GetSalesOrdersQueryHandler sut,
            GetSalesOrdersQuery query
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'salesOrdersResult')");

            mockSalesOrderApiClient.Verify(_ => _.GetSalesOrdersAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string?>(),
                    It.IsAny<CustomerType>()
                )
            );
        }
    }
}