using AW.Core.Abstractions.Api.CustomerApi.AddCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerContact;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerContactInfo;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContact;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContactInfo;
using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using AW.Infrastructure.Http;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace AW.Infrastructure.Api.REST.UnitTests
{
    public class CustomerApiUnitTests
    {
        [Fact]
        public async void AddCustomerAddress_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Post(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
            var baseAddress = "BaseAddress";

            var sut = new CustomerApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            await sut.AddCustomerAddressAsync(new AddCustomerAddressRequest
                {
                    AccountNumber = "1",
                    CustomerAddress = new Core.Abstractions.Api.CustomerApi.AddCustomerAddress.CustomerAddress
                    {
                        AddressType = "Home",
                        Address = new Core.Abstractions.Api.CustomerApi.AddCustomerAddress.Address
                        {
                            City = "Seattle"
                        }
                    }
                }
            );

            //Assert
            mockHttpRequestFactory.Verify(x => x.Post(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()));
        }

        [Fact]
        public async void AddCustomerContact_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Post(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
            var baseAddress = "BaseAddress";

            var sut = new CustomerApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            await sut.AddCustomerContactAsync(new AddCustomerContactRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Post(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()));
        }

        [Fact]
        public async void AddCustomerContactInfo_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Post(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
            var baseAddress = "BaseAddress";

            var sut = new CustomerApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            await sut.AddCustomerContactInfoAsync(new AddCustomerContactInfoRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Post(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteCustomerAddress_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Post(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
            var baseAddress = "BaseAddress";

            var sut = new CustomerApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            await sut.DeleteCustomerAddressAsync(new DeleteCustomerAddressRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Post(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteCustomerContact_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Post(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
            var baseAddress = "BaseAddress";

            var sut = new CustomerApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            await sut.DeleteCustomerContactAsync(new DeleteCustomerContactRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Post(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()));
        }

        [Fact]
        public async void DeleteCustomerContactInfo_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Post(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
            var baseAddress = "BaseAddress";

            var sut = new CustomerApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            await sut.DeleteCustomerContactInfoAsync(new DeleteCustomerContactInfoRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Post(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetCustomer_ReturnsCustomer()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new GetCustomerResponse
                {
                    Customer = new Core.Abstractions.Api.CustomerApi.GetCustomer.Customer
                    {
                        AccountNumber = "1"
                    }
                })
            });

            var baseAddress = "BaseAddress";

            var sut = new CustomerApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            var response = await sut.GetCustomerAsync(new GetCustomerRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()));

            response.Customer.AccountNumber.Should().Be("1");
        }

        [Fact]
        public async void ListCustomers_ReturnsCustomers()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerApi>>();
            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new ListCustomersResponse
                {
                    Customers = new List<Core.Abstractions.Api.CustomerApi.ListCustomers.Customer>
                    {
                        new Core.Abstractions.Api.CustomerApi.ListCustomers.Customer { AccountNumber = "1" },
                        new Core.Abstractions.Api.CustomerApi.ListCustomers.Customer { AccountNumber = "2" }
                    },
                    TotalCustomers = 2
                })
            });

            var baseAddress = "BaseAddress";

            var sut = new CustomerApi(
                mockLogger.Object,
                mockHttpRequestFactory.Object,
                baseAddress
            );

            //Act
            var response = await sut.ListCustomersAsync(new ListCustomersRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()));
            response.TotalCustomers.Should().Be(2);
            response.Customers[0].AccountNumber.Should().Be("1");
            response.Customers[1].AccountNumber.Should().Be("2");
        }
    }
}