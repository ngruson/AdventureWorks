using AW.Core.Abstractions.Api.CustomerApi.AddCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerContact;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerContactInfo;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContact;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContactInfo;
using DeleteCustomerContact = AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContact;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using UpdateCustomer = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using UpdateCustomerContact = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerContact;
using AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.ListCustomers;
using UpdateCustomerContact_TB = AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.UpdateCustomerContact;
using AW.Infrastructure.Http;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;
using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerContact;

namespace AW.Infrastructure.Api.REST.UnitTests
{
    public class CustomerApiUnitTests
    {
        #region ListCustomers helpers
        private ListCustomers.Customer ListCustomer_Store()
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
                    .Contacts(new List<ListCustomers.CustomerContact>
                        { 
                            new CustomerContactBuilder()
                                .ContactType("Owner")
                                .Contact(new ContactBuilder()
                                    .Title("Mr.")
                                    .FirstName("Orlando")
                                    .MiddleName("N.")
                                    .LastName("")
                                    .Build()
                            )
                            .Build(),

                            new CustomerContactBuilder()
                                .ContactType("Order Administrator")
                                .Contact(new ContactBuilder()
                                    .Title("Mr.")
                                    .FirstName("Orlando")
                                    .MiddleName("N.")
                                    .LastName("")
                                    .Build()
                            )
                            .Build(),
                        }
                    )
                    .Build()
                )
                .Build();
        }

        private ListCustomers.Customer ListCustomer_Person()
        {
            return new CustomerBuilder()
                .AccountNumber("AW00011000")
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
                    .ContactInfo(new List<ListCustomers.ContactInfo>
                        {
                            new TestBuilders.CustomerApi.ListCustomers.ContactInfoBuilder()
                                .Channel(Core.Domain.Person.ContactInfoChannelType.Email)
                                .Value("jon24@adventure-works.com")
                                .Build(),

                            new TestBuilders.CustomerApi.ListCustomers.ContactInfoBuilder()
                                .Channel(Core.Domain.Person.ContactInfoChannelType.Phone)
                                .ContactInfoType("Home")
                                .Value("1 (11) 500 555-0162")
                                .Build()
                        }
                    )
                    .Build()
                )
                .Build();
        }

        #endregion

        #region GetCustomer helpers
        private GetCustomer.Customer GetCustomer_Store()
        {
            return new TestBuilders.CustomerApi.GetCustomer.CustomerBuilder()
                .AccountNumber("AW00000001")
                .Store(new TestBuilders.CustomerApi.GetCustomer.StoreBuilder()
                    .Name("A Bike Store")
                    .Addresses(new List<GetCustomer.CustomerAddress>
                        {
                            new TestBuilders.CustomerApi.GetCustomer.CustomerAddressBuilder()
                                .Address(new TestBuilders.CustomerApi.GetCustomer.AddressBuilder()
                                    .AddressLine1("2251 Elliot Avenue")
                                    .City("Seattle")
                                    .StateProvince(new TestBuilders.CustomerApi.GetCustomer.StateProvinceBuilder()
                                        .StateProvinceCode("WA")
                                        .Name("Washington")
                                        .Build()
                                    )
                                    .Build()
                                )
                                .Build()
                        }
                    )
                    .Contacts(new List<GetCustomer.CustomerContact>
                        {
                            new TestBuilders.CustomerApi.GetCustomer.CustomerContactBuilder()
                                .ContactType("Owner")
                                .Contact(new TestBuilders.CustomerApi.GetCustomer.ContactBuilder()
                                    .Title("Mr.")
                                    .FirstName("Orlando")
                                    .MiddleName("N.")
                                    .LastName("")
                                    .Build()
                            )
                            .Build(),

                            new TestBuilders.CustomerApi.GetCustomer.CustomerContactBuilder()
                                .ContactType("Order Administrator")
                                .Contact(new TestBuilders.CustomerApi.GetCustomer.ContactBuilder()
                                    .Title("Mr.")
                                    .FirstName("Orlando")
                                    .MiddleName("N.")
                                    .LastName("")
                                    .Build()
                            )
                            .Build(),
                        }
                    )
                    .Build()
                )
                .Build();
        }

        private GetCustomer.Customer GetCustomer_Person()
        {
            return new TestBuilders.CustomerApi.GetCustomer.CustomerBuilder()
                .AccountNumber("AW00011000")
                .Person(new TestBuilders.CustomerApi.GetCustomer.PersonBuilder()
                    .FirstName("Jon")
                    .MiddleName("V")
                    .LastName("Yang")
                    .Addresses(new List<GetCustomer.CustomerAddress>
                        {
                            new TestBuilders.CustomerApi.GetCustomer.CustomerAddressBuilder()
                                .Address(new TestBuilders.CustomerApi.GetCustomer.AddressBuilder()
                                    .AddressLine1("3761 N. 14th St")
                                    .City("Rockhampton")
                                    .StateProvince(new TestBuilders.CustomerApi.GetCustomer.StateProvinceBuilder()
                                        .StateProvinceCode("QLD")
                                        .Name("Queensland")
                                        .Build()
                                    )
                                    .Build()
                                )
                                .Build()
                        }
                    )
                    .ContactInfo(new List<GetCustomer.ContactInfo>
                        {
                            new TestBuilders.CustomerApi.GetCustomer.ContactInfoBuilder()
                                .Channel(Core.Domain.Person.ContactInfoChannelType.Email)
                                .Value("jon24@adventure-works.com")
                                .Build(),

                            new TestBuilders.CustomerApi.GetCustomer.ContactInfoBuilder()
                                .Channel(Core.Domain.Person.ContactInfoChannelType.Phone)
                                .ContactInfoType("Home")
                                .Value("1 (11) 500 555-0162")
                                .Build()
                        }
                    )
                    .Build()
                )
                .Build();
        }
        #endregion

        #region UpdateCustomer helpers
        private UpdateCustomer.Customer UpdateCustomer_Store()
        {
            return new TestBuilders.CustomerApi.UpdateCustomer.CustomerBuilder()
                .AccountNumber("AW00000001")
                .Store(new TestBuilders.CustomerApi.UpdateCustomer.StoreBuilder()
                    .Name("A Bike Store")
                    .Addresses(new List<UpdateCustomer.CustomerAddress>
                        {
                            new TestBuilders.CustomerApi.UpdateCustomer.CustomerAddressBuilder()
                                .Address(new TestBuilders.CustomerApi.UpdateCustomer.AddressBuilder()
                                    .AddressLine1("2251 Elliot Avenue")
                                    .City("Seattle")
                                    .StateProvince(new TestBuilders.CustomerApi.UpdateCustomer.StateProvinceBuilder()
                                        .StateProvinceCode("WA")
                                        .Name("Washington")
                                        .Build()
                                    )
                                    .Build()
                                )
                                .Build()
                        }
                    )
                    .Contacts(new List<UpdateCustomer.CustomerContact>
                        {
                            new TestBuilders.CustomerApi.UpdateCustomer.CustomerContactBuilder()
                                .ContactType("Owner")
                                .Contact(new TestBuilders.CustomerApi.UpdateCustomer.ContactBuilder()
                                    .Title("Mr.")
                                    .FirstName("Orlando")
                                    .MiddleName("N.")
                                    .LastName("")
                                    .Build()
                            )
                            .Build(),

                            new TestBuilders.CustomerApi.UpdateCustomer.CustomerContactBuilder()
                                .ContactType("Order Administrator")
                                .Contact(new TestBuilders.CustomerApi.UpdateCustomer.ContactBuilder()
                                    .Title("Mr.")
                                    .FirstName("Orlando")
                                    .MiddleName("N.")
                                    .LastName("")
                                    .Build()
                            )
                            .Build(),
                        }
                    )
                    .Build()
                )
                .Build();
        }

        private UpdateCustomer.Customer UpdateCustomer_Person()
        {
            return new TestBuilders.CustomerApi.UpdateCustomer.CustomerBuilder()
                .AccountNumber("AW00011000")
                .Person(new TestBuilders.CustomerApi.UpdateCustomer.PersonBuilder()
                    .FirstName("Jon")
                    .MiddleName("V")
                    .LastName("Yang")
                    .Addresses(new List<UpdateCustomer.CustomerAddress>
                        {
                            new TestBuilders.CustomerApi.UpdateCustomer.CustomerAddressBuilder()
                                .Address(new TestBuilders.CustomerApi.UpdateCustomer.AddressBuilder()
                                    .AddressLine1("3761 N. 14th St")
                                    .City("Rockhampton")
                                    .StateProvince(new TestBuilders.CustomerApi.UpdateCustomer.StateProvinceBuilder()
                                        .StateProvinceCode("QLD")
                                        .Name("Queensland")
                                        .Build()
                                    )
                                    .Build()
                                )
                                .Build()
                        }
                    )
                    .ContactInfo(new List<UpdateCustomer.ContactInfo>
                        {
                            new TestBuilders.CustomerApi.UpdateCustomer.ContactInfoBuilder()
                                .Channel(Core.Domain.Person.ContactInfoChannelType.Email)
                                .Value("jon24@adventure-works.com")
                                .Build(),

                            new TestBuilders.CustomerApi.UpdateCustomer.ContactInfoBuilder()
                                .Channel(Core.Domain.Person.ContactInfoChannelType.Phone)
                                .ContactInfoType("Home")
                                .Value("1 (11) 500 555-0162")
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
            await sut.DeleteCustomerContactAsync(new DeleteCustomerContactRequest
                {
                    AccountNumber = "AW00000001",
                    ContactType = "Order Administrator",
                    Contact = new TestBuilders.CustomerApi.DeleteCustomerContact.ContactBuilder().WithTestValues().Build()
                }
            );

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
        public async void GetCustomer_Store_ReturnsCustomer()
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
                    Customer = GetCustomer_Store()
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

            response.Customer.AccountNumber.Should().Be("AW00000001");
        }

        [Fact]
        public async void GetCustomer_Person_ReturnsCustomer()
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
                    Customer = GetCustomer_Person()
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

            response.Customer.AccountNumber.Should().Be("AW00011000");
        }

        [Fact]
        public async void ListCustomers_ReturnsCustomers()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerApi>>();

            var customers = new List<ListCustomers.Customer>
            {
                ListCustomer_Store(),
                ListCustomer_Person()
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
            response.Customers[1].AccountNumber.Should().Be("AW00011000");
        }

        [Fact]
        public async void UpdateCustomer_Store_OK()
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
            await sut.UpdateCustomerAsync(new UpdateCustomerRequest
                {
                    Customer = UpdateCustomer_Store()
                }
            );

            //Assert
            mockHttpRequestFactory.Verify(x => x.Post(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()));
        }

        [Fact]
        public async void UpdateCustomer_Person_OK()
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
            await sut.UpdateCustomerAsync(new UpdateCustomerRequest
                {
                    Customer = UpdateCustomer_Person()
                }
            );

            //Assert
            mockHttpRequestFactory.Verify(x => x.Post(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()));
        }

        [Fact]
        public async void UpdateCustomerAddress_OK()
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
            await sut.UpdateCustomerAddressAsync(new UpdateCustomerAddressRequest
                {
                    AccountNumber = "1",
                    CustomerAddress = new Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress.CustomerAddress
                    {
                        AddressType = "Home",
                        Address = new Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress.Address
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
        public async void UpdateCustomerContact_OK()
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
            await sut.UpdateCustomerContactAsync(new UpdateCustomerContactRequest
                {
                    AccountNumber = "AW00000001",
                    CustomerContact = new UpdateCustomerContact_TB.CustomerContactBuilder().WithTestValues().Build()
                }
            );

            //Assert
            mockHttpRequestFactory.Verify(x => x.Post(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<string>()));
        }
    }
}