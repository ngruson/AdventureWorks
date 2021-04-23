using AW.UI.Web.Internal.UnitTests.AutoMapper;
using AW.UI.Web.Internal.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Linq;
using Xunit;
using System.Collections.Generic;
using AW.UI.Web.Internal.UnitTests.TestBuilders.GetCustomer;
using c = AW.UI.Web.Internal.ApiClients.CustomerApi;
using r = AW.UI.Web.Internal.ApiClients.ReferenceDataApi;
using AW.UI.Web.Internal.Services;
using AW.UI.Web.Internal.ApiClients.ReferenceDataApi;
using AW.UI.Web.Internal.ApiClients.SalesPersonApi;
using AW.UI.Web.Internal.ApiClients.CustomerApi;
using Microsoft.Extensions.Logging;
using AW.UI.Web.Internal.ViewModels.Customer;
using AW.UI.Web.Internal.UnitTests.TestBuilders.GetTerritories;

namespace AW.UI.Web.Internal.UnitTests
{
    public class CustomerServiceUnitTests
    {
        [Fact]
        public async void GetCustomers_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            var customers = new List<c.Models.GetCustomers.Customer>();
            for (int i = 1; i <= 10; i++)
            {
                string accountNumber = "AW" + (i.ToString().PadLeft(8, '0'));
                customers.Add(
                    new TestBuilders.GetCustomers.StoreCustomerBuilder()
                        .AccountNumber(accountNumber).Build()
                );
            }

            mockCustomerApi.Setup(x => x.GetCustomersAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<c.Models.GetCustomers.CustomerType>(),
                It.IsAny<string>()
            ))
            .ReturnsAsync(new c.Models.GetCustomers.GetCustomersResponse
            {
                Customers = customers,
                TotalCustomers = 100
            });

            mockReferenceDataApi.Setup(x => x.GetTerritoriesAsync()
            )
            .ReturnsAsync(new List<r.Models.GetTerritories.Territory>
            {
                new SalesTerritoryBuilder().CountryRegion("US").Name("Northwest").Build(),
                new SalesTerritoryBuilder().CountryRegion("US").Name("Northeast").Build()
            });

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = await svc.GetCustomers(0, 10, null, c.Models.GetCustomers.CustomerType.Store, null);

            //Assert
            viewModel.Customers.Count.Should().Be(10);
            viewModel.Territories.ToList().Count.Should().Be(3);
            viewModel.Territories.ToList()[0].Text.Should().Be("All");
            viewModel.CustomerTypes.Count().Should().Be(3);
            viewModel.PaginationInfo.Should().NotBeNull();
            viewModel.PaginationInfo.ActualPage.Should().Be(0);
            viewModel.PaginationInfo.ItemsPerPage.Should().Be(10);
            viewModel.PaginationInfo.TotalItems.Should().Be(100);
            viewModel.PaginationInfo.TotalPages.Should().Be(10);
            viewModel.PaginationInfo.Next.Should().Be("");
            viewModel.PaginationInfo.Previous.Should().Be("disabled");
        }

        [Fact]
        public async void GetCustomer_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockCustomerApi.Setup(x => x.GetCustomerAsync(It.IsAny<string>()))
            .ReturnsAsync(new StoreCustomerBuilder()
                .AccountNumber("AW00000001")
                .Build()
            );

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = await svc.GetCustomer("AW00000001");

            //Assert
            viewModel.Customer.AccountNumber.Should().Be("AW00000001");
        }

        [Fact]
        public async void GetIndividualCustomerForEdit_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockCustomerApi.Setup(x => x.GetCustomerAsync<c.Models.GetCustomer.IndividualCustomer>(It.IsAny<string>()))
           .ReturnsAsync(new IndividualCustomerBuilder()
                .WithTestValues()
                .Build()
            );
            
            mockReferenceDataApi.Setup(x => x.GetTerritoriesAsync()
            )
            .ReturnsAsync(new List<r.Models.GetTerritories.Territory>
            {
                new SalesTerritoryBuilder().CountryRegion("US").Name("Northwest").Build(),
                new SalesTerritoryBuilder().CountryRegion("US").Name("Northeast").Build()
            });

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = await svc.GetIndividualCustomerForEdit("AW00000001");

            //Assert
            viewModel.Customer.AccountNumber.Should().Be("AW00000001");
            viewModel.Territories.ToList().Count.Should().Be(3);
            viewModel.Territories.ToList()[0].Text.Should().Be("--Select--");
            viewModel.EmailPromotions.Count().Should().Be(4);
            viewModel.EmailPromotions.ToList()[0].Text.Should().Be("All");
        }

        [Fact]
        public async void UpdateStore_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            
            mockCustomerApi.Setup(x => x.GetCustomerAsync<c.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
           .ReturnsAsync(new StoreCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = new StoreCustomerViewModel {
                AccountNumber = "AW00000001"
            };
            await svc.UpdateStore(viewModel);

            //Assert
            mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                It.IsAny<string>(),
                It.IsAny<c.Models.UpdateCustomer.Customer>())
            );
        }

        [Fact]
        public async void UpdateStore_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            
            mockCustomerApi.Setup(x => x.GetCustomerAsync<c.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
           .ReturnsAsync(new StoreCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = new StoreCustomerViewModel
            {
                AccountNumber = "AW00000001"
            };
            await svc.UpdateStore(viewModel);

            //Assert
            mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                It.IsAny<string>(),
                It.IsAny<c.Models.UpdateCustomer.Customer>())
            );
        }

        [Fact]
        public async void UpdateStore_WithSalesPerson_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();

            mockCustomerApi.Setup(x => x.GetCustomerAsync<c.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
           .ReturnsAsync(new StoreCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockSalesPersonApi.Setup(x => x.GetSalesPersonAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            ))
            .ReturnsAsync(new ApiClients.SalesPersonApi.Models.SalesPerson());

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = new StoreCustomerViewModel
            {
                AccountNumber = "AW00000001",
                SalesPerson = "Stephen Y. Jiang"
            };
            await svc.UpdateStore(viewModel);

            //Assert
            mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                It.IsAny<string>(),
                It.IsAny<c.Models.UpdateCustomer.Customer>())
            );
        }

        [Fact]
        public async void UpdateIndividual_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = new IndividualCustomerViewModel
            {
                AccountNumber = "AW00000001"
            };
            await svc.UpdateIndividual(viewModel);

            //Assert
            mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                It.IsAny<string>(),
                It.IsAny<c.Models.UpdateCustomer.Customer>())
            );
        }

        [Fact]
        public void AddAddress_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();            
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();

            mockReferenceDataApi.Setup(x => x.GetAddressTypesAsync())
            .ReturnsAsync(
                new string[] { "Main Office", "Home" }
                    .Select(x => new r.Models.GetAddressTypes.AddressType
                    {
                        Name = x
                    })
                    .ToList()
           );

            mockReferenceDataApi.Setup(x => x.GetCountriesAsync())
            .ReturnsAsync(new List<r.Models.GetCountries.CountryRegion>()
            {
                new r.Models.GetCountries.CountryRegion
                {
                    CountryRegionCode = "US",
                    Name = "United States"
                },
                new r.Models.GetCountries.CountryRegion
                {
                    CountryRegionCode = "GB",
                    Name = "United Kingdom"
                }
            });

            mockReferenceDataApi.Setup(x => x.GetStateProvincesAsync(
                It.IsAny<string>()
            ))
            .ReturnsAsync(new List<r.Models.GetStateProvinces.StateProvince>()
            {
                new r.Models.GetStateProvinces.StateProvince
                {
                    CountryRegionCode = "US",
                    Name = "Alaska"
                },
                new r.Models.GetStateProvinces.StateProvince
                {
                    CountryRegionCode = "US",
                    Name = "North Carolina"
                },
                new r.Models.GetStateProvinces.StateProvince
                {
                    CountryRegionCode = "CA",
                    Name = "Brunswick"
                }
            });

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = svc.AddAddress("AW00000001", "A Bike Store");

            //Assert
            viewModel.IsNewAddress.Should().Be(true);
        }

        [Fact]
        public async void AddAddress_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockCustomerApi.Setup(x => x.GetCustomerAsync(It.IsAny<string>()))
            .ReturnsAsync(
                new StoreCustomerBuilder()
                    .WithTestValues()
                    .Build()
            );

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = new EditCustomerAddressViewModel
            {
                CustomerAddress = new CustomerAddressViewModel
                {
                    AddressType = "Main Office",
                    Address = new AddressViewModel
                    {
                        AddressLine1 = "2251 Elliot Avenue",
                        PostalCode = "98104",
                        City = "Seattle",
                        StateProvinceCode = "WA",
                        CountryRegionCode = "US"
                    }
                }
            };
            await svc.AddAddress(viewModel);

            //Assert
            mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                It.IsAny<string>(),
                It.IsAny<c.Models.UpdateCustomer.Customer>()));
        }

        [Fact]
        public async void GetCustomerAddress_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockReferenceDataApi.Setup(x => x.GetAddressTypesAsync())
            .ReturnsAsync(new List<r.Models.GetAddressTypes.AddressType>
            {
                new r.Models.GetAddressTypes.AddressType { Name = "Billing" },
                new r.Models.GetAddressTypes.AddressType { Name = "Home" },
                new r.Models.GetAddressTypes.AddressType { Name = "Main Office" }
            });

            mockReferenceDataApi.Setup(x => x.GetCountriesAsync())
            .ReturnsAsync(new List<r.Models.GetCountries.CountryRegion>
            {
                new r.Models.GetCountries.CountryRegion { CountryRegionCode = "US", Name = "United States" },
                new r.Models.GetCountries.CountryRegion { CountryRegionCode = "GB", Name = "United Kingdom" }
            });

            mockReferenceDataApi.Setup(x => x.GetStateProvincesAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<r.Models.GetStateProvinces.StateProvince>
            {
                new r.Models.GetStateProvinces.StateProvince 
                { 
                    CountryRegionCode = "US", 
                    StateProvinceCode = "CA", 
                    Name = "California" 
                },
                new r.Models.GetStateProvinces.StateProvince
                {
                    CountryRegionCode = "US",
                    StateProvinceCode = "TX",
                    Name = "Texas"
                }
            });

            mockCustomerApi.Setup(x => x.GetCustomerAsync(It.IsAny<string>()))
            .ReturnsAsync(
                new StoreCustomerBuilder()
                    .Name("A Bike Store")
                    .WithTestValues()
                    .Build()
            );

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = await svc.GetCustomerAddress("AW00000001", "Main Office");

            //Assert
            viewModel.AccountNumber.Should().Be("AW00000001");
            viewModel.CustomerName.Should().Be("A Bike Store");
            viewModel.CustomerAddress.Should().NotBeNull();
        }

        [Fact]
        public async void UpdateAddress_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockCustomerApi.Setup(x => x.GetCustomerAsync(It.IsAny<string>()))
            .ReturnsAsync(
                new StoreCustomerBuilder()
                    .WithTestValues()
                    .Build()
            );

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = new EditCustomerAddressViewModel
            {
                AccountNumber = "AW00000001",
                CustomerAddress = new CustomerAddressViewModel
                {
                    AddressType = "Main Office",
                    Address = new AddressViewModel
                    {
                        AddressLine1 = "2251 Elliot Avenue",
                        PostalCode = "98104",
                        City = "Seattle",
                        StateProvinceCode = "WA",
                        CountryRegionCode = "US"
                    }
                }
            };
            await svc.UpdateAddress(viewModel);

            //Assert
            mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                It.IsAny<string>(),
                It.IsAny<c.Models.UpdateCustomer.Customer>()
            ));
        }

        [Fact]
        public async void GetCustomerAddressForDelete_Store_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockCustomerApi.Setup(x => x.GetCustomerAsync(It.IsAny<string>()))
            .ReturnsAsync(
                new StoreCustomerBuilder()
                    .Name("A Bike Store")
                    .Addresses(new List<c.Models.GetCustomer.CustomerAddress>
                    {
                        new CustomerAddressBuilder()
                            .AddressTypeName("Main Office")
                            .Address(new AddressBuilder()
                                .City("Seattle")
                                .StateProvinceCode("WA")
                                .CountryRegionCode("US")
                                .Build()
                            )
                            .Build()
                    })
                    .Build()
            );

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = await svc.GetCustomerAddressForDelete("AW00000001", "Main Office");

            //Assert
            viewModel.CustomerName = "A Bike Store";
        }

        [Fact]
        public async void GetCustomerAddressForDelete_Person_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockCustomerApi
                .Setup(x => x.GetCustomerAsync(It.IsAny<string>()))
                .ReturnsAsync(new IndividualCustomerBuilder()
                    .WithTestValues()
                    .Build()
                );

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = await svc.GetCustomerAddressForDelete("AW00000002", "Home");

            //Assert
            viewModel.CustomerName = "Jon V Yang";
        }

        [Fact]
        public async void GetStateProvincesJson_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockReferenceDataApi.Setup(x => x.GetStateProvincesAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<r.Models.GetStateProvinces.StateProvince>
                {
                    new r.Models.GetStateProvinces.StateProvince
                    {
                        CountryRegionCode = "US", StateProvinceCode = "AZ", Name = "Arizona"
                    },
                    new r.Models.GetStateProvinces.StateProvince
                    {
                        CountryRegionCode = "US", StateProvinceCode = "CA", Name = "California"
                    }
                });

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = await svc.GetStateProvincesJson("US");

            //Assert
            viewModel.Count().Should().Be(2);
        }

        [Fact]
        public async void DeleteAddress_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockCustomerApi
                .Setup(x => x.GetCustomerAsync(It.IsAny<string>()))
                .ReturnsAsync(new IndividualCustomerBuilder()
                    .WithTestValues()
                    .Build()
                );

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            await svc.DeleteAddress("AW00000001", "Home");

            //Assert
            mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                It.IsAny<string>(),
                It.IsAny<c.Models.UpdateCustomer.Customer>()
            ));
        }

        [Fact]
        public async void AddContact_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockReferenceDataApi.Setup(x => x.GetContactTypesAsync())
                .ReturnsAsync(new List<r.Models.GetContactTypes.ContactType>
                    {
                        new r.Models.GetContactTypes.ContactType { Name = "Owner" },
                        new r.Models.GetContactTypes.ContactType { Name = "Marketing Assistant" },
                        new r.Models.GetContactTypes.ContactType { Name = "Order Administrator" }
                    });

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = await svc.AddContact("AW00000001", "A Bike Store");

            //Assert
            viewModel.IsNewContact.Should().Be(true);
            viewModel.ContactTypes.Count().Should().Be(4);
            viewModel.ContactTypes.ToList()[0].Text.Should().Be("--Select--");
        }

        [Fact]
        public async void AddContact_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockReferenceDataApi.Setup(x => x.GetContactTypesAsync())
                .ReturnsAsync(new List<r.Models.GetContactTypes.ContactType>
                    {
                        new r.Models.GetContactTypes.ContactType { Name = "Owner" },
                        new r.Models.GetContactTypes.ContactType { Name = "Marketing Assistant" },
                        new r.Models.GetContactTypes.ContactType { Name = "Order Administrator" }
                    });

            mockCustomerApi
                .Setup(x => x.GetCustomerAsync<c.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
                .ReturnsAsync(new StoreCustomerBuilder()
                    .WithTestValues()
                    .Build()
                );

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = new EditCustomerContactViewModel
            {
                CustomerContact = new CustomerContactViewModel
                {
                    ContactType = "Owner",
                    ContactPerson = new PersonViewModel
                    {
                        FirstName = "Orlando",
                        MiddleName = "N.",
                        LastName = "Gee"
                    }
                }
            };
            await svc.AddContact(viewModel);

            //Assert
            mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                It.IsAny<string>(),
                It.IsAny<c.Models.UpdateCustomer.Customer>()
            ));
        }

        [Fact]
        public async void GetCustomerContact_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockReferenceDataApi.Setup(x => x.GetContactTypesAsync())
                .ReturnsAsync(new List<r.Models.GetContactTypes.ContactType>
                    {
                        new r.Models.GetContactTypes.ContactType { Name = "Owner" },
                        new r.Models.GetContactTypes.ContactType { Name = "Marketing Assistant" },
                        new r.Models.GetContactTypes.ContactType { Name = "Order Administrator" }
                    });

            mockCustomerApi
                .Setup(x => x.GetCustomerAsync<c.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
                .ReturnsAsync(new StoreCustomerBuilder()
                    .Name("A Bike Store")
                    .Contacts(new List<c.Models.GetCustomer.StoreCustomerContact>
                    {
                        new StoreCustomerContactBuilder()
                            .WithTestValues()
                            .Build()
                    })
                    .Build()
                );

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = await svc.GetCustomerContact("AW00000001", "Orlando N. Gee", "Order Administrator");

            //Assert            
            viewModel.IsNewContact.Should().Be(false);
            viewModel.ContactTypes.Count().Should().Be(4);
            viewModel.ContactTypes.ToList()[0].Text.Should().Be("--Select--");
        }

        [Fact]
        public async void UpdateContact_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockCustomerApi.Setup(x => x.GetCustomerAsync<c.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
                .ReturnsAsync(new StoreCustomerBuilder()
                    .WithTestValues()
                    .Build()
                );

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = new EditCustomerContactViewModel
            {
                AccountNumber = "AW00000001",
                CustomerContact = new CustomerContactViewModel
                {
                    ContactType = "Owner",
                    ContactPerson = new PersonViewModel
                    {
                        FirstName = "Orlando",
                        MiddleName = "N.",
                        LastName = "Gee"
                    }
                }
            };
            await svc.UpdateContact(viewModel);

            //Assert
            mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                It.IsAny<string>(),
                It.IsAny<c.Models.UpdateCustomer.Customer>()
            ));
        }

        [Fact]
        public async void GetCustomerContactForDelete_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockCustomerApi.Setup(x => x.GetCustomerAsync<c.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
                .ReturnsAsync(new StoreCustomerBuilder()
                    .WithTestValues()
                    .Build()
                );

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = await svc.GetCustomerContactForDelete("AW00000001", "Orlando N. Gee", "Owner");

            //Assert
            viewModel.AccountNumber.Should().Be("AW00000001");
            viewModel.CustomerName.Should().Be("A Bike Store");
            viewModel.ContactType.Should().Be("Owner");
        }

        [Fact]
        public async void DeleteContact_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockCustomerApi
                .Setup(x => x.GetCustomerAsync<c.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
                .ReturnsAsync(new StoreCustomerBuilder()
                    .WithTestValues()
                    .Build()
                );

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = new DeleteCustomerContactViewModel
            {
                AccountNumber = "AW00000001",
                CustomerName = "A Bike Store",
                ContactPerson = new PersonViewModel
                {
                    FirstName = "Orlando",
                    MiddleName = "N.",
                    LastName = "Gee"
                },
                ContactType = "Owner"
            };
            await svc.DeleteContact(viewModel);

            //Assert
            mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                It.IsAny<string>(),
                It.IsAny<c.Models.UpdateCustomer.Customer>()
            ));
        }

        [Fact]
        public void AddContactEmailAddress_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockReferenceDataApi.Setup(x => x.GetContactTypesAsync())
                .ReturnsAsync(new List<r.Models.GetContactTypes.ContactType>
                    {
                        new r.Models.GetContactTypes.ContactType { Name = "Owner" },
                        new r.Models.GetContactTypes.ContactType { Name = "Marketing Assistant" },
                        new r.Models.GetContactTypes.ContactType { Name = "Order Administrator" }
                    });

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = svc.AddEmailAddress("AW00000001", "Orlando N. Gee");

            //Assert
            viewModel.IsNewEmailAddress.Should().Be(true);
        }

        [Fact]
        public async void AddContactEmailAddress_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<ISalesPersonApiClient>();

            mockCustomerApi
                .Setup(x => x.GetCustomerAsync<c.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
                .ReturnsAsync(new StoreCustomerBuilder()
                    .WithTestValues()
                    .Build()
                );

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = new EditEmailAddressViewModel
            {
                IsNewEmailAddress = true,
                AccountNumber = "AW00000001",
                PersonName = "Orlando N. Gee",
                EmailAddress = "orlando0@adventure-works.com"
            };
            await svc.AddContactEmailAddress(viewModel);

            //Assert
            mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                It.IsAny<string>(),
                It.IsAny<c.Models.UpdateCustomer.Customer>()
            ));
        }
    }
}