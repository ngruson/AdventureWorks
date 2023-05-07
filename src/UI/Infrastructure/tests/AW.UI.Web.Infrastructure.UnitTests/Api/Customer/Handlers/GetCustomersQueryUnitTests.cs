using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomers;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Customer.Handlers
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
            mockCustomerApiClient.Setup(_ => _.GetCustomersAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string?>(),
                    It.IsAny<AW.SharedKernel.Interfaces.CustomerType?>(),
                    It.IsAny<string?>()
                )
            )
            .ReturnsAsync((GetCustomersResponse?)null);

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
