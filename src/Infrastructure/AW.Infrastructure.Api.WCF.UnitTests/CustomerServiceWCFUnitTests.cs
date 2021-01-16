using AutoMapper;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerContact;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerContactInfo;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContact;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContactInfo;
using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using AW.Infrastructure.Api.WCF.AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AW.Infrastructure.Api.WCF.UnitTests
{
    public class CustomerServiceWCFUnitTests
    {
        [Fact]
        public async void AddCustomerAddress_OK()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CustomerProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<CustomerServiceWCF>>();
            var mockCustomerService = new Mock<CustomerService.ICustomerService>();

            var sut = new CustomerServiceWCF(
                mockLogger.Object,
                mapper,
                mockCustomerService.Object
            );

            //Act
            await sut.AddCustomerAddressAsync(new AddCustomerAddressRequest());

            //Assert
            mockCustomerService.Verify(x => x.AddCustomerAddressAsync(It.IsAny<CustomerService.AddCustomerAddressRequest>()));
        }

        [Fact]
        public async void AddCustomerContact_OK()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CustomerProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<CustomerServiceWCF>>();
            var mockCustomerService = new Mock<CustomerService.ICustomerService>();

            var sut = new CustomerServiceWCF(
                mockLogger.Object,
                mapper,
                mockCustomerService.Object
            );

            //Act
            await sut.AddCustomerContactAsync(new AddCustomerContactRequest());

            //Assert
            mockCustomerService.Verify(x => x.AddCustomerContactAsync(It.IsAny<CustomerService.AddCustomerContactRequest>()));
        }

        [Fact]
        public async void AddCustomerContactInfo_OK()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CustomerProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<CustomerServiceWCF>>();
            var mockCustomerService = new Mock<CustomerService.ICustomerService>();

            var sut = new CustomerServiceWCF(
                mockLogger.Object,
                mapper,
                mockCustomerService.Object
            );

            //Act
            await sut.AddCustomerContactInfoAsync(new AddCustomerContactInfoRequest());

            //Assert
            mockCustomerService.Verify(x => x.AddCustomerContactInfoAsync(It.IsAny<CustomerService.AddCustomerContactInfoRequest>()));
        }

        [Fact]
        public async void DeleteCustomerAddress_OK()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CustomerProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<CustomerServiceWCF>>();
            var mockCustomerService = new Mock<CustomerService.ICustomerService>();

            var sut = new CustomerServiceWCF(
                mockLogger.Object,
                mapper,
                mockCustomerService.Object
            );

            //Act
            await sut.DeleteCustomerAddressAsync(new DeleteCustomerAddressRequest());

            //Assert
            mockCustomerService.Verify(x => x.DeleteCustomerAddressAsync(It.IsAny<CustomerService.DeleteCustomerAddressRequest>()));
        }

        [Fact]
        public async void DeleteCustomerContact_OK()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CustomerProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<CustomerServiceWCF>>();
            var mockCustomerService = new Mock<CustomerService.ICustomerService>();

            var sut = new CustomerServiceWCF(
                mockLogger.Object,
                mapper,
                mockCustomerService.Object
            );

            //Act
            await sut.DeleteCustomerContactAsync(new DeleteCustomerContactRequest());

            //Assert
            mockCustomerService.Verify(x => x.DeleteCustomerContactAsync(It.IsAny<CustomerService.DeleteCustomerContactRequest>()));
        }

        [Fact]
        public async void DeleteCustomerContactInfo_OK()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CustomerProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<CustomerServiceWCF>>();
            var mockCustomerService = new Mock<CustomerService.ICustomerService>();

            var sut = new CustomerServiceWCF(
                mockLogger.Object,
                mapper,
                mockCustomerService.Object
            );

            //Act
            await sut.DeleteCustomerContactInfoAsync(new DeleteCustomerContactInfoRequest());

            //Assert
            mockCustomerService.Verify(x => x.DeleteCustomerContactInfoAsync(It.IsAny<CustomerService.DeleteCustomerContactInfoRequest>()));
        }

        [Fact]
        public async void GetCustomer_ReturnsCustomer()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CustomerProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<CustomerServiceWCF>>();
            var mockCustomerService = new Mock<CustomerService.ICustomerService>();
            mockCustomerService.Setup(x => x.GetCustomerAsync(It.IsAny<CustomerService.GetCustomerRequest>()))
                .ReturnsAsync(new CustomerService.GetCustomerResponseGetCustomerResult
                {
                    Customer = new CustomerService.Customer1
                    {
                        AccountNumber = "1"
                    }                        
                });

            var sut = new CustomerServiceWCF(
                mockLogger.Object,
                mapper,
                mockCustomerService.Object
            );

            //Act
            var response = await sut.GetCustomerAsync(new GetCustomerRequest());

            //Assert
            mockCustomerService.Verify(x => x.GetCustomerAsync(It.IsAny<CustomerService.GetCustomerRequest>()));
            
            response.Customer.AccountNumber.Should().Be("1");
        }

        [Fact]
        public async void ListCustomers_ReturnsCustomers()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CustomerProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<CustomerServiceWCF>>();
            var mockCustomerService = new Mock<CustomerService.ICustomerService>();
            mockCustomerService.Setup(x => x.ListCustomersAsync(It.IsAny<CustomerService.ListCustomersRequest>()))
                .ReturnsAsync(new CustomerService.ListCustomersResponseListCustomersResult
                {
                    Customers = new CustomerService.Customer[] {
                        new CustomerService.Customer
                        {
                            AccountNumber = "1"
                        },
                        new CustomerService.Customer
                        {
                            AccountNumber = "2"
                        }
                    },
                    TotalCustomers = 2
                });

            var sut = new CustomerServiceWCF(
                mockLogger.Object,
                mapper,
                mockCustomerService.Object
            );

            //Act
            var response = await sut.ListCustomersAsync(new ListCustomersRequest());

            //Assert
            mockCustomerService.Verify(x => x.ListCustomersAsync(It.IsAny<CustomerService.ListCustomersRequest>()));
            response.TotalCustomers.Should().Be(2);
            response.Customers[0].AccountNumber.Should().Be("1");
            response.Customers[1].AccountNumber.Should().Be("2");
        }
    }
}