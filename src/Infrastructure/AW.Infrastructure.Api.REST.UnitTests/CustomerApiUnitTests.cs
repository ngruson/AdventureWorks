using AW.Core.Abstractions.Api.CustomerApi.AddCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerContact;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerContactInfo;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContact;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContactInfo;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.ListCustomers;
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
        #region ListCustomers helpers
        private ListCustomers.Customer Customer1()
        {
            return new CustomerBuilder()
                .AccountNumber("AW00000001")
                .Store(new StoreBuilder()
                    .Name("A Bike Store")
                    .Addresses(new List<ListCustomers.CustomerAddress>
                        {
                            new CustomerAddressBuilder()
                                .Address(new AddressBuilder()
                                    .AddressLine1("2251 Elliot Avenue")
                                    .City("Seattle")
                                    .StateProvince(new StateProvinceBuilder()
                                        .StateProvinceCode("WA")
                                        .Name("Washington")
                                        .Build()
                                    )
                                    .Build()
                                )
                                .Build()
                        }
                    )
                    .Build()
                )
                .Build();
        }

        private ListCustomers.Customer Customer2()
        {
            return new CustomerBuilder()
                .AccountNumber("AW00000002")
                .Person(new PersonBuilder()
                    .FirstName("Jon")
                    .MiddleName("V")
                    .LastName("Yang")
                    .Addresses(new List<ListCustomers.CustomerAddress>
                        {
                            new CustomerAddressBuilder()
                                .Address(new AddressBuilder()
                                    .AddressLine1("3761 N. 14th St")
                                    .City("Rockhampton")
                                    .StateProvince(new StateProvinceBuilder()
                                        .StateProvinceCode("QLD")
                                        .Name("Queensland")
                                        .Build()
                                    )
                                    .Build()
                                )
                                .Build()
                        }
                    )
                    .Build()
                )
                .Build();
        }

        #endregion

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
            await sut.AddCustomerContactAsync(new AddCustomerContactRequest
                {
                    AccountNumber = "1",
                    CustomerContact = new Core.Abstractions.Api.CustomerApi.AddCustomerContact.CustomerContact
                    {
                        Contact = new Core.Abstractions.Api.CustomerApi.AddCustomerContact.Contact
                        {
                            FirstName = "John"
                        },
                        ContactType = "Owner"
                    }
                }
            );

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
            await sut.AddCustomerContactInfoAsync(new AddCustomerContactInfoRequest
                {
                    AccountNumber = "1",
                    CustomerContactInfo = new Core.Abstractions.Api.CustomerApi.AddCustomerContactInfo.CustomerContactInfo
                    {
                        Channel = Core.Abstractions.Api.CustomerApi.AddCustomerContactInfo.Channel.Email,
                        Value = "test@mail.com"
                    }
                }
            );

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
                Content = new JsonContent(new GetCustomer.GetCustomerResponse
                {
                    Customer = new GetCustomer.Customer
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
            var response = await sut.GetCustomerAsync(new GetCustomer.GetCustomerRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()));

            response.Customer.AccountNumber.Should().Be("1");
        }

        [Fact]
        public async void ListCustomers_ReturnsCustomers()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerApi>>();

            var customers = new List<ListCustomers.Customer>
            {
                Customer1(),
                Customer2()
            };

            var mockHttpRequestFactory = new Mock<IHttpRequestFactory>();
            mockHttpRequestFactory.Setup(x => x.Get(
                    It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()
                )
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(new ListCustomers.ListCustomersResponse
                {
                    Customers = customers,
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
            var response = await sut.ListCustomersAsync(new ListCustomers.ListCustomersRequest());

            //Assert
            mockHttpRequestFactory.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()));
            response.TotalCustomers.Should().Be(2);
            response.Customers[0].AccountNumber.Should().Be("AW00000001");
            response.Customers[1].AccountNumber.Should().Be("AW00000002");
        }
    }
}