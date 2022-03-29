using AW.UI.Web.Internal.UnitTests.AutoMapper;
using FluentAssertions;
using Moq;
using System.Linq;
using Xunit;
using System.Collections.Generic;
using AW.UI.Web.Internal.UnitTests.TestBuilders.GetCustomer;
using customerApi = AW.UI.Web.Infrastructure.ApiClients.CustomerApi;
using referenceDataApi = AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi;
using salesPersonApi = AW.UI.Web.Infrastructure.ApiClients.SalesPersonApi;
using AW.UI.Web.Internal.Services;
using Microsoft.Extensions.Logging;
using AW.UI.Web.Internal.ViewModels.Customer;
using AW.UI.Web.Internal.UnitTests.TestBuilders.GetTerritories;
using AW.SharedKernel.Interfaces;
using System.Threading.Tasks;
using AW.SharedKernel.UnitTesting;
using AutoFixture.Xunit2;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetTerritories;

namespace AW.UI.Web.Internal.UnitTests.Services
{
    public class CustomerServiceUnitTests
    {
        public class GetCustomers
        {            
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetCustomers_FirstPage_ReturnsViewModel(
                [Frozen] Mock<customerApi.ICustomerApiClient> mockCustomerApi,
                [Frozen] Mock<referenceDataApi.IReferenceDataApiClient> mockReferenceDataApi,
                CustomerService sut,                
                List<Territory> territories
            )
            {
                //Arrange
                var customers = Enumerable.Repeat(new customerApi.Models.GetCustomers.StoreCustomer(), 10).ToList();

                mockCustomerApi.Setup(x => x.GetCustomersAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<CustomerType>(),
                    It.IsAny<string>()
                ))
                .ReturnsAsync(new customerApi.Models.GetCustomers.GetCustomersResponse
                {
                    Customers = customers.Cast<customerApi.Models.GetCustomers.Customer>().ToList(),
                    TotalCustomers = customers.Count * 10
                });

                mockReferenceDataApi.Setup(x => x.GetTerritoriesAsync())
                    .ReturnsAsync(territories);

                //Act
                var viewModel = await sut.GetCustomers(0, 10, null, CustomerType.Store, null);

                //Assert
                viewModel.Customers.Count.Should().Be(10);
                viewModel.Territories.ToList().Count.Should().Be(4);
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

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetCustomers_LastPage_ReturnsViewModel(
                [Frozen] Mock<customerApi.ICustomerApiClient> mockCustomerApi,
                [Frozen] Mock<referenceDataApi.IReferenceDataApiClient> mockReferenceDataApi,
                CustomerService sut,
                List<Territory> territories
            )
            {
                //Arrange
                var customers = Enumerable.Repeat(new customerApi.Models.GetCustomers.StoreCustomer(), 10).ToList();

                mockCustomerApi.Setup(x => x.GetCustomersAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<CustomerType>(),
                    It.IsAny<string>()
                ))
                .ReturnsAsync(new customerApi.Models.GetCustomers.GetCustomersResponse
                {
                    Customers = customers.Cast<customerApi.Models.GetCustomers.Customer>().ToList(),
                    TotalCustomers = customers.Count * 10
                });

                mockReferenceDataApi.Setup(x => x.GetTerritoriesAsync())
                    .ReturnsAsync(territories);

                //Act
                var viewModel = await sut.GetCustomers(9, 10, null, CustomerType.Store, null);

                //Assert
                viewModel.Customers.Count.Should().Be(10);
                viewModel.Territories.ToList().Count.Should().Be(4);
                viewModel.Territories.ToList()[0].Text.Should().Be("All");
                viewModel.CustomerTypes.Count().Should().Be(3);
                viewModel.PaginationInfo.Should().NotBeNull();
                viewModel.PaginationInfo.ActualPage.Should().Be(9);
                viewModel.PaginationInfo.ItemsPerPage.Should().Be(10);
                viewModel.PaginationInfo.TotalItems.Should().Be(100);
                viewModel.PaginationInfo.TotalPages.Should().Be(10);
                viewModel.PaginationInfo.Next.Should().Be("disabled");
                viewModel.PaginationInfo.Previous.Should().Be("");
            }
        }

        public class GetStoreCustomerForEdit
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetStoreCustomerForEdit_ReturnsViewModel(
                [Frozen] Mock<customerApi.ICustomerApiClient> mockCustomerApiClient,
                [Frozen] Mock<referenceDataApi.IReferenceDataApiClient> mockReferenceDataApiClient,
                [Frozen] Mock<salesPersonApi.ISalesPersonApiClient> mockSalesPersonApiClient,
                CustomerService sut,
                customerApi.Models.GetCustomer.StoreCustomer customer,
                List<Territory> territories,
                List<salesPersonApi.Models.SalesPerson> salesPersons
            )
            {
                //Arrange
                mockCustomerApiClient.Setup(x => x
                    .GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(
                        It.IsAny<string>()
                ))
               .ReturnsAsync(customer);

                mockReferenceDataApiClient.Setup(x => x.GetTerritoriesAsync())
                    .ReturnsAsync(territories);

                mockSalesPersonApiClient.Setup(_ => _.GetSalesPersonsAsync(It.IsAny<string>()))
                    .ReturnsAsync(salesPersons);

                //Act
                var viewModel = await sut.GetStoreCustomerForEdit(customer.AccountNumber);

                //Assert
                viewModel.Customer.AccountNumber.Should().Be(customer.AccountNumber);
                viewModel.Territories.ToList().Count.Should().Be(4);
                viewModel.Territories.ToList()[0].Text.Should().Be("--Select--");
                viewModel.SalesPersons.ToList().Count.Should().Be(4);
                viewModel.SalesPersons.ToList()[0].Text.Should().Be("All");
            }
        }

        [Fact]
        public async Task GetCustomer_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

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
        public async Task GetIndividualCustomerForEdit_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

            mockCustomerApi.Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.IndividualCustomer>(It.IsAny<string>()))
           .ReturnsAsync(new IndividualCustomerBuilder()
                .WithTestValues()
                .Build()
            );
            
            mockReferenceDataApi.Setup(x => x.GetTerritoriesAsync()
            )
            .ReturnsAsync(new List<referenceDataApi.Models.GetTerritories.Territory>
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
        public async Task UpdateStore_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            
            mockCustomerApi.Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
           .ReturnsAsync(new StoreCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();

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
                It.IsAny<customerApi.Models.UpdateCustomer.Customer>())
            );
        }

        [Fact]
        public async Task UpdateStore_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            
            mockCustomerApi.Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
           .ReturnsAsync(new StoreCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();

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
                It.IsAny<customerApi.Models.UpdateCustomer.Customer>())
            );
        }

        [Fact]
        public async Task UpdateStore_WithSalesPerson_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();

            mockCustomerApi.Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
           .ReturnsAsync(new StoreCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

            mockSalesPersonApi.Setup(x => x.GetSalesPersonAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            ))
            .ReturnsAsync(new salesPersonApi.Models.SalesPerson());

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
                It.IsAny<customerApi.Models.UpdateCustomer.Customer>())
            );
        }

        [Fact]
        public async Task UpdateIndividual_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

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
                It.IsAny<customerApi.Models.UpdateCustomer.Customer>())
            );
        }

        [Fact]
        public void AddAddress_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();            
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();

            mockReferenceDataApi.Setup(x => x.GetAddressTypesAsync())
            .ReturnsAsync(
                new string[] { "Main Office", "Home" }
                    .Select(x => new referenceDataApi.Models.GetAddressTypes.AddressType
                    {
                        Name = x
                    })
                    .ToList()
           );

            mockReferenceDataApi.Setup(x => x.GetCountriesAsync())
            .ReturnsAsync(new List<referenceDataApi.Models.GetCountries.CountryRegion>()
            {
                new referenceDataApi.Models.GetCountries.CountryRegion
                {
                    CountryRegionCode = "US",
                    Name = "United States"
                },
                new referenceDataApi.Models.GetCountries.CountryRegion
                {
                    CountryRegionCode = "GB",
                    Name = "United Kingdom"
                }
            });

            mockReferenceDataApi.Setup(x => x.GetStatesProvincesAsync(
                It.IsAny<string>()
            ))
            .ReturnsAsync(new List<referenceDataApi.Models.GetStateProvinces.StateProvince>()
            {
                new referenceDataApi.Models.GetStateProvinces.StateProvince
                {
                    CountryRegionCode = "US",
                    Name = "Alaska"
                },
                new referenceDataApi.Models.GetStateProvinces.StateProvince
                {
                    CountryRegionCode = "US",
                    Name = "North Carolina"
                },
                new referenceDataApi.Models.GetStateProvinces.StateProvince
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
        public async Task AddAddress_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

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
                It.IsAny<customerApi.Models.UpdateCustomer.Customer>()));
        }

        [Fact]
        public async Task GetCustomerAddress_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

            mockReferenceDataApi.Setup(x => x.GetAddressTypesAsync())
            .ReturnsAsync(new List<referenceDataApi.Models.GetAddressTypes.AddressType>
            {
                new referenceDataApi.Models.GetAddressTypes.AddressType { Name = "Billing" },
                new referenceDataApi.Models.GetAddressTypes.AddressType { Name = "Home" },
                new referenceDataApi.Models.GetAddressTypes.AddressType { Name = "Main Office" }
            });

            mockReferenceDataApi.Setup(x => x.GetCountriesAsync())
            .ReturnsAsync(new List<referenceDataApi.Models.GetCountries.CountryRegion>
            {
                new referenceDataApi.Models.GetCountries.CountryRegion { CountryRegionCode = "US", Name = "United States" },
                new referenceDataApi.Models.GetCountries.CountryRegion { CountryRegionCode = "GB", Name = "United Kingdom" }
            });

            mockReferenceDataApi.Setup(x => x.GetStatesProvincesAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<referenceDataApi.Models.GetStateProvinces.StateProvince>
            {
                new referenceDataApi.Models.GetStateProvinces.StateProvince 
                { 
                    CountryRegionCode = "US", 
                    StateProvinceCode = "CA", 
                    Name = "California" 
                },
                new referenceDataApi.Models.GetStateProvinces.StateProvince
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
        public async Task UpdateAddress_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

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
                It.IsAny<customerApi.Models.UpdateCustomer.Customer>()
            ));
        }

        [Fact]
        public async Task GetCustomerAddressForDelete_Store_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

            mockCustomerApi.Setup(x => x.GetCustomerAsync(It.IsAny<string>()))
            .ReturnsAsync(
                new StoreCustomerBuilder()
                    .Name("A Bike Store")
                    .Addresses(new List<customerApi.Models.GetCustomer.CustomerAddress>
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
            viewModel.CustomerName.Should().Be("A Bike Store");
        }

        [Fact]
        public async Task GetCustomerAddressForDelete_Person_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

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
            viewModel.CustomerName.Should().Be("Jon V Yang");
        }

        [Fact]
        public async Task GetStateProvincesJson_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

            mockReferenceDataApi.Setup(x => x.GetStatesProvincesAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<referenceDataApi.Models.GetStateProvinces.StateProvince>
                {
                    new referenceDataApi.Models.GetStateProvinces.StateProvince
                    {
                        CountryRegionCode = "US", StateProvinceCode = "AZ", Name = "Arizona"
                    },
                    new referenceDataApi.Models.GetStateProvinces.StateProvince
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
            var viewModel = await svc.GetStatesProvincesJson("US");

            //Assert
            viewModel.Count().Should().Be(2);
        }

        [Fact]
        public async Task DeleteAddress_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

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
                It.IsAny<customerApi.Models.UpdateCustomer.Customer>()
            ));
        }

        [Fact]
        public async Task AddContact_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

            mockReferenceDataApi.Setup(x => x.GetContactTypesAsync())
                .ReturnsAsync(new List<referenceDataApi.Models.GetContactTypes.ContactType>
                    {
                        new referenceDataApi.Models.GetContactTypes.ContactType { Name = "Owner" },
                        new referenceDataApi.Models.GetContactTypes.ContactType { Name = "Marketing Assistant" },
                        new referenceDataApi.Models.GetContactTypes.ContactType { Name = "Order Administrator" }
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
        public async Task AddContact_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

            mockReferenceDataApi.Setup(x => x.GetContactTypesAsync())
                .ReturnsAsync(new List<referenceDataApi.Models.GetContactTypes.ContactType>
                    {
                        new referenceDataApi.Models.GetContactTypes.ContactType { Name = "Owner" },
                        new referenceDataApi.Models.GetContactTypes.ContactType { Name = "Marketing Assistant" },
                        new referenceDataApi.Models.GetContactTypes.ContactType { Name = "Order Administrator" }
                    });

            mockCustomerApi
                .Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
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
                        Name = new PersonNameViewModel
                        {
                            FirstName = "Orlando",
                            MiddleName = "N.",
                            LastName = "Gee"
                        }
                    }
                }
            };
            await svc.AddContact(viewModel);

            //Assert
            mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                It.IsAny<string>(),
                It.IsAny<customerApi.Models.UpdateCustomer.Customer>()
            ));
        }

        [Fact]
        public async Task GetCustomerContact_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

            mockReferenceDataApi.Setup(x => x.GetContactTypesAsync())
                .ReturnsAsync(new List<referenceDataApi.Models.GetContactTypes.ContactType>
                    {
                        new referenceDataApi.Models.GetContactTypes.ContactType { Name = "Owner" },
                        new referenceDataApi.Models.GetContactTypes.ContactType { Name = "Marketing Assistant" },
                        new referenceDataApi.Models.GetContactTypes.ContactType { Name = "Order Administrator" }
                    });

            mockCustomerApi
                .Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
                .ReturnsAsync(new StoreCustomerBuilder()
                    .Name("A Bike Store")
                    .Contacts(new List<customerApi.Models.GetCustomer.StoreCustomerContact>
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
        public async Task UpdateContact_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

            mockCustomerApi.Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
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
                        Name = new PersonNameViewModel
                        {
                            FirstName = "Orlando",
                            MiddleName = "N.",
                            LastName = "Gee"
                        }
                    }
                }
            };
            await svc.UpdateContact(viewModel);

            //Assert
            mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                It.IsAny<string>(),
                It.IsAny<customerApi.Models.UpdateCustomer.Customer>()
            ));
        }

        [Fact]
        public async Task GetCustomerContactForDelete_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

            mockCustomerApi.Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
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
        public async Task DeleteContact_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

            mockCustomerApi
                .Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
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
                    Name = new PersonNameViewModel
                    {
                        FirstName = "Orlando",
                        MiddleName = "N.",
                        LastName = "Gee"
                    }
                },
                ContactType = "Owner"
            };
            await svc.DeleteContact(viewModel);

            //Assert
            mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                It.IsAny<string>(),
                It.IsAny<customerApi.Models.UpdateCustomer.Customer>()
            ));
        }

        [Fact]
        public void AddContactEmailAddress_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

            mockReferenceDataApi.Setup(x => x.GetContactTypesAsync())
                .ReturnsAsync(new List<referenceDataApi.Models.GetContactTypes.ContactType>
                    {
                        new referenceDataApi.Models.GetContactTypes.ContactType { Name = "Owner" },
                        new referenceDataApi.Models.GetContactTypes.ContactType { Name = "Marketing Assistant" },
                        new referenceDataApi.Models.GetContactTypes.ContactType { Name = "Order Administrator" }
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
        public async Task AddContactEmailAddress_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

            mockCustomerApi
                .Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
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
                It.IsAny<customerApi.Models.UpdateCustomer.Customer>()
            ));
        }

        [Fact]
        public void AddContactPhoneNumber_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

            mockReferenceDataApi.Setup(x => x.GetContactTypesAsync())
                .ReturnsAsync(new List<referenceDataApi.Models.GetContactTypes.ContactType>
                    {
                        new referenceDataApi.Models.GetContactTypes.ContactType { Name = "Owner" },
                        new referenceDataApi.Models.GetContactTypes.ContactType { Name = "Marketing Assistant" },
                        new referenceDataApi.Models.GetContactTypes.ContactType { Name = "Order Administrator" }
                    });

            var svc = new CustomerService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockCustomerApi.Object,
                mockReferenceDataApi.Object,
                mockSalesPersonApi.Object
            );

            //Act
            var viewModel = svc.AddPhoneNumber("AW00000001", "Orlando N. Gee");

            //Assert
            viewModel.IsNewPhoneNumber.Should().Be(true);
        }

        [Fact]
        public async Task AddContactPhoneNumber_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerService>>();
            var mockCustomerApi = new Mock<customerApi.ICustomerApiClient>();
            var mockReferenceDataApi = new Mock<referenceDataApi.IReferenceDataApiClient>();
            var mockSalesPersonApi = new Mock<salesPersonApi.ISalesPersonApiClient>();

            mockCustomerApi
                .Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(It.IsAny<string>()))
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
            var viewModel = new EditPhoneNumberViewModel
            {
                IsNewPhoneNumber = true,
                AccountNumber = "AW00000001",
                PersonName = "Orlando N. Gee",
                PhoneNumberType = "Cell",
                PhoneNumber = "245-555-0173"
            };
            await svc.AddContactPhoneNumber(viewModel);

            //Assert
            mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                It.IsAny<string>(),
                It.IsAny<customerApi.Models.UpdateCustomer.Customer>()
            ));
        }
    }
}