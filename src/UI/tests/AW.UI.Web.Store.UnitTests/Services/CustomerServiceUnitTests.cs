using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients.CustomerApi;
using AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Exceptions;
using AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.GetCustomer;
using AW.UI.Web.Store.Services;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Store.UnitTests.Services
{
    public class CustomerServiceUnitTests
    {
        public class GetCustomer
        {
            [Theory, AutoMoqData]
            public async Task GetCustomer_CustomerExists_ReturnsCustomer(
                CustomerService sut,
                Customer customer,
                string customerNumber
            )
            {
                //Arrange

                //Act
                var response = await sut.GetCustomerAsync(customerNumber);
                    
                //Assert
                response.Should().BeEquivalentTo(customer);
            }

            [Theory, AutoMoqData]
            public async Task GetCustomer_ApiError_ReturnsNull(
                [Frozen] Mock<ICustomerApiClient> mockClient,
                CustomerService sut,
                string customerNumber                
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetCustomerAsync(It.IsAny<string>()))
                    .Throws<CustomerApiClientException>();

                //Act
                var response = await sut.GetCustomerAsync(customerNumber);

                //Assert
                response.Should().BeNull();
            }
        }

        public class GetPreferredAddress
        {
            [Theory, AutoMoqData]
            public async Task GetPreferredAddress_AddressExists_ReturnsAddress(
                [Frozen] Mock<ICustomerApiClient> mockClient,
                CustomerService sut,
                Infrastructure.ApiClients.CustomerApi.Models.GetPreferredAddress.Address address,
                string customerNumber,
                string addressType
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetPreferredAddressAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync(address);

                //Act
                var response = await sut.GetPreferredAddressAsync(customerNumber, addressType);

                //Assert
                response.Should().BeEquivalentTo(address);
            }

            [Theory, AutoMoqData]
            public async Task GetPreferredAddress_ApiError_ReturnsNull(
                [Frozen] Mock<ICustomerApiClient> mockClient,
                CustomerService sut,
                string customerNumber,
                string addressType
            )
            {
                //Arrange
                mockClient.Setup(_ => _.GetPreferredAddressAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .Throws<CustomerApiClientException>();

                //Act
                var response = await sut.GetPreferredAddressAsync(customerNumber, addressType);

                //Assert
                response.Should().BeNull();
            }
        }
    }
}