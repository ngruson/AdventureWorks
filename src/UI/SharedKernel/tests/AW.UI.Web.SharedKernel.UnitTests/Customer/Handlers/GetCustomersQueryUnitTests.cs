using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomers;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Customer.Handlers
{
    public class GetCustomersQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_ReturnedCustomersNotNull_CustomersReturned(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetCustomersQueryHandler sut,
            GetCustomersQuery query,
            GetCustomersResponse customersResponse
        )
        {
            //Arrange
            mockCustomerApiClient.Setup(_ => _.GetCustomersAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string?>(),
                    It.IsAny<AW.SharedKernel.Interfaces.CustomerType?>(),
                    It.IsAny<string?>()
                )
            )
            .ReturnsAsync(customersResponse);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(customersResponse);

            mockCustomerApiClient.Verify(_ => _.GetCustomersAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string?>(),
                    It.IsAny<AW.SharedKernel.Interfaces.CustomerType?>(),
                    It.IsAny<string?>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_ReturnedCustomersNull_ThrowsArgumentNullException(
            [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
            GetCustomersQueryHandler sut,
            GetCustomersQuery query
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customers')");

            mockCustomerApiClient.Verify(_ => _.GetCustomersAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string?>(),
                    It.IsAny<AW.SharedKernel.Interfaces.CustomerType?>(),
                    It.IsAny<string?>()
                )
            );
        }
    }
}
