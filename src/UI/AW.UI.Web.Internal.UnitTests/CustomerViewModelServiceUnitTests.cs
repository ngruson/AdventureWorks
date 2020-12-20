﻿using AW.Core.Abstractions.Api.CustomerApi;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using UpdateCustomer = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using AddCustomerAddress = AW.Core.Abstractions.Api.CustomerApi.AddCustomerAddress;
using UpdateCustomerAddress = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress;
using DeleteCustomerAddress = AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerAddress;
using AddCustomerContact = AW.Core.Abstractions.Api.CustomerApi.AddCustomerContact;
using UpdateCustomerContact = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerContact;
using DeleteCustomerContact = AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContact;
using AddCustomerContactInfo = AW.Core.Abstractions.Api.CustomerApi.AddCustomerContactInfo;
using DeleteCustomerContactInfo = AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContactInfo;
using AW.UI.Web.Internal.Services;
using AW.UI.Web.Internal.UnitTests.AutoMapper;
using AW.UI.Web.Internal.UnitTests.TestBuilders;
using AW.UI.Web.Internal.ViewModels.Customer;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using Xunit;
using System.Collections.Generic;
using AW.UI.Web.Internal.UnitTests.TestBuilders.GetCustomer;
using AW.Infrastructure.Api.WCF.AddressTypeService;
using AW.Infrastructure.Api.WCF.ContactTypeService;
using AW.Infrastructure.Api.WCF.CountryService;
using AW.Infrastructure.Api.WCF.SalesPersonService;
using AW.Infrastructure.Api.WCF.SalesTerritoryService;
using AW.Infrastructure.Api.WCF.StateProvinceService;

namespace AW.UI.Web.Internal.UnitTests
{
    public class CustomerViewModelServiceUnitTests
    {
        [Fact]
        public async void GetCustomers_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();
            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            mockCustomerService.Setup(x => x.ListCustomersAsync(It.IsAny<ListCustomers.ListCustomersRequest>()))
                .ReturnsAsync(new ListCustomers.ListCustomersResponse
                {
                    Customers = new List<ListCustomers.Customer>
                    {
                        new TestBuilders.ListCustomers.CustomerBuilder().AccountNumber("AW00000001").Build(),
                        new TestBuilders.ListCustomers.CustomerBuilder().AccountNumber("AW00000002").Build(),
                        new TestBuilders.ListCustomers.CustomerBuilder().AccountNumber("AW00000003").Build(),
                        new TestBuilders.ListCustomers.CustomerBuilder().AccountNumber("AW00000004").Build(),
                        new TestBuilders.ListCustomers.CustomerBuilder().AccountNumber("AW00000005").Build()
                    },
                    TotalCustomers = 100
                });
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();

            var countryRegion = new CountryRegionDto
            {
                CountryRegionCode = "US",
                Name = "United States"
            };
            mockSalesTerritoryService.Setup(x => x.ListTerritoriesAsync(It.IsAny<ListTerritoriesRequest>()))
                .ReturnsAsync(new ListTerritoriesResponse
                {
                    ListTerritoriesResult = new TerritoryDto[]
                    {
                        new SalesTerritoryBuilder().CountryRegion(countryRegion).Name("Northwest").Build(),
                        new SalesTerritoryBuilder().CountryRegion(countryRegion).Name("Northeast").Build()
                    }
                });
            var mockSalesPersonService = new Mock<ISalesPersonService>();
            var mockStateProvinceService = new Mock<IStateProvinceService>();

            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = await svc.GetCustomers(0, 10, null, null);

            //Assert
            viewModel.Customers.Count.Should().Be(5);
            viewModel.Territories.ToList().Count.Should().Be(3);
            viewModel.Territories.ToList()[0].Text.Should().Be("All");
            viewModel.CustomerTypes.Count().Should().Be(3);
            viewModel.PaginationInfo.Should().NotBeNull();
            viewModel.PaginationInfo.ActualPage.Should().Be(0);
            viewModel.PaginationInfo.ItemsPerPage.Should().Be(5);
            viewModel.PaginationInfo.TotalItems.Should().Be(100);
            viewModel.PaginationInfo.TotalPages.Should().Be(10);
            viewModel.PaginationInfo.Next.Should().Be("");
            viewModel.PaginationInfo.Previous.Should().Be("disabled");
        }

        [Fact]
        public async void GetCustomer_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();
            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();

            var mockCustomerService = new Mock<ICustomerApi>();
            mockCustomerService.Setup(x => x.GetCustomerAsync(It.IsAny<GetCustomer.GetCustomerRequest>()))
                .ReturnsAsync(new GetCustomer.GetCustomerResponse
                {
                    Customer = new GetCustomer.Customer { AccountNumber = "AW00000001" }
                });
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();
            var mockStateProvinceService = new Mock<IStateProvinceService>();

            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
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
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();
            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();

            var mockCustomerService = new Mock<ICustomerApi>();
            mockCustomerService.Setup(x => x.GetCustomerAsync(It.IsAny<GetCustomer.GetCustomerRequest>()))
                .ReturnsAsync(new GetCustomer.GetCustomerResponse
                {
                    Customer = new GetCustomer.Customer { AccountNumber = "AW00000001" }
                });

            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var countryRegion = new CountryRegionDto
            {
                CountryRegionCode = "US",
                Name = "United States"
            };
            mockSalesTerritoryService.Setup(x => x.ListTerritoriesAsync(It.IsAny<ListTerritoriesRequest>()))
                .ReturnsAsync(new ListTerritoriesResponse
                {
                    ListTerritoriesResult = new TerritoryDto[]
                    {
                        new SalesTerritoryBuilder().CountryRegion(countryRegion).Name("Northwest").Build(),
                        new SalesTerritoryBuilder().CountryRegion(countryRegion).Name("Northeast").Build()
                    }
                });

            var mockSalesPersonService = new Mock<ISalesPersonService>();
            var mockStateProvinceService = new Mock<IStateProvinceService>();

            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
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
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();
            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();
            var mockStateProvinceService = new Mock<IStateProvinceService>();

            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = new CustomerViewModel {
                AccountNumber = "AW00000001",
                Store = new CustomerStoreViewModel()
            };
            await svc.UpdateStore(viewModel);

            //Assert
            mockCustomerService.Verify(x => x.UpdateCustomerAsync(It.IsAny<UpdateCustomer.UpdateCustomerRequest>()));
        }

        [Fact]
        public async void UpdateStore_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();
            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();
            var mockStateProvinceService = new Mock<IStateProvinceService>();

            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = new CustomerViewModel
            {
                AccountNumber = "AW00000001",
                Store = new CustomerStoreViewModel()
            };
            await svc.UpdateStore(viewModel);

            //Assert
            mockCustomerService.Verify(x => x.UpdateCustomerAsync(It.IsAny<UpdateCustomer.UpdateCustomerRequest>()));
        }

        [Fact]
        public async void UpdateStore_WithSalesPerson_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();
            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();
            mockSalesPersonService.Setup(x => x.GetSalesPersonAsync(It.IsAny<GetSalesPersonRequest>()))
                .ReturnsAsync(new GetSalesPersonResponseGetSalesPersonResult
                {
                    SalesPerson = new SalesPersonDto1()
                });

            var mockStateProvinceService = new Mock<IStateProvinceService>();

            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = new CustomerViewModel
            {
                AccountNumber = "AW00000001",
                Store = new CustomerStoreViewModel
                {
                    SalesPerson = new SalesPersonViewModel
                    {
                        FullName = "Stephen Y. Jiang"
                    }
                }
            };
            await svc.UpdateStore(viewModel);

            //Assert

            mockCustomerService.Verify(x => x.UpdateCustomerAsync(It.IsAny<UpdateCustomer.UpdateCustomerRequest>()));
        }

        [Fact]
        public async void UpdateIndividual_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();
            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();
            var mockStateProvinceService = new Mock<IStateProvinceService>();

            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = new CustomerViewModel
            {
                AccountNumber = "AW00000001",
                Person = new CustomerPersonViewModel()
            };
            await svc.UpdateIndividual(viewModel);

            //Assert
            mockCustomerService.Verify(x => x.UpdateCustomerAsync(It.IsAny<UpdateCustomer.UpdateCustomerRequest>()));
        }

        [Fact]
        public async void AddAddress_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();
            
            var mockAddressTypeService = new Mock<IAddressTypeService>();
            mockAddressTypeService.Setup(x => x.ListAddressTypesAsync())
                .ReturnsAsync(new ListAddressTypesResponse
                {
                    AddressTypes = new string[] { "Billing", "Home", "Main Office" }
                });

            var mockContactTypeService = new Mock<IContactTypeService>();
            
            var mockCountryService = new Mock<ICountryService>();
            mockCountryService.Setup(x => x.ListCountriesAsync())
                .ReturnsAsync(new ListCountriesResponse
                {
                    Countries = new CountryDto[]
                    {
                        new CountryDto { CountryRegionCode = "US", Name = "United States"},
                        new CountryDto { CountryRegionCode = "GB", Name = "United Kingdom"}
                    }
                });

            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();
            
            var mockStateProvinceService = new Mock<IStateProvinceService>();
            mockStateProvinceService.Setup(x => x.ListStateProvincesAsync(It.IsAny<ListStateProvincesRequest>()))
                .ReturnsAsync(new ListStateProvincesResponse
                {
                    StateProvinces = new StateProvince[]
                    {
                        new StateProvince { CountryRegionCode = "US", Name = "Alaska" },
                        new StateProvince { CountryRegionCode = "US", Name = "North Carolina" },
                        new StateProvince { CountryRegionCode = "CA", Name = "Brunswick" }
                    }
                });

            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = await svc.AddAddress("AW00000001", "A Bike Store");

            //Assert
            viewModel.IsNewAddress.Should().Be(true);
            viewModel.AddressTypes.Count().Should().Be(4);
            viewModel.AddressTypes.ToList()[0].Text.Should().Be("--Select--");
            viewModel.Countries.Count().Should().Be(3);
            viewModel.Countries.ToList()[0].Text.Should().Be("--Select--");
            viewModel.StateProvinces.Count().Should().Be(4);
            viewModel.StateProvinces.ToList()[0].Text.Should().Be("--Select--");
        }

        [Fact]
        public async void AddAddress_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();

            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();

            var mockStateProvinceService = new Mock<IStateProvinceService>();
            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = new EditCustomerAddressViewModel
            {

            };
            await svc.AddAddress(viewModel);

            //Assert
            mockCustomerService.Verify(x => x.AddCustomerAddressAsync(It.IsAny<AddCustomerAddress.AddCustomerAddressRequest>()));
        }

        [Fact]
        public async void GetCustomerAddress_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();

            var mockAddressTypeService = new Mock<IAddressTypeService>();
            mockAddressTypeService.Setup(x => x.ListAddressTypesAsync())
               .ReturnsAsync(new ListAddressTypesResponse
               {
                   AddressTypes = new string[] { "Billing", "Home", "Main Office" }
               });

            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();
            mockCountryService.Setup(x => x.ListCountriesAsync())
                .ReturnsAsync(new ListCountriesResponse
                {
                    Countries = new CountryDto[]
                    {
                        new CountryDto { CountryRegionCode = "US", Name = "United States"},
                        new CountryDto { CountryRegionCode = "GB", Name = "United Kingdom"}
                    }
                });

            var mockCustomerService = new Mock<ICustomerApi>();
            mockCustomerService
                .Setup(x => x.GetCustomerAsync(It.IsAny<GetCustomer.GetCustomerRequest>()))
                .ReturnsAsync(new GetCustomer.GetCustomerResponse
                {
                    Customer = new CustomerBuilder()
                        .Store(new StoreBuilder()
                            .Name("A Bike Store")
                            .Addresses(new CustomerAddressBuilder()
                                .AddressTypeName("Main Office")
                                .Address(new AddressBuilder()
                                    .StateProvince(new StateProvinceBuilder()
                                        .StateProvinceCode("AZ")
                                        .Name("Arizona")
                                        .CountryRegion(new CountryRegionBuilder()
                                            .CountryRegionCode("US")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                                )
                                .Build()
                            )
                            .Build()
                        )
                        .Build()
                });

            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();

            var mockStateProvinceService = new Mock<IStateProvinceService>();
            mockStateProvinceService
                .Setup(x => x.ListStateProvincesAsync(It.IsAny<ListStateProvincesRequest>()))
                .ReturnsAsync(new ListStateProvincesResponse
                {
                    StateProvinces = new Infrastructure.Api.WCF.StateProvinceService.StateProvince[]
                    {
                        new StateProvince { CountryRegionCode = "US", StateProvinceCode = "AZ", Name = "Arizona" },
                        new StateProvince { CountryRegionCode = "US", StateProvinceCode = "CA", Name = "California" }
                    }
                });

            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = await svc.GetCustomerAddress("AW00000001", "Main Office");

            //Assert            
            viewModel.AddressTypes.Count().Should().Be(4);
            viewModel.AddressTypes.ToList()[0].Text.Should().Be("--Select--");
            viewModel.Countries.Count().Should().Be(3);
            viewModel.Countries.ToList()[0].Text.Should().Be("--Select--");
            viewModel.StateProvinces.Count().Should().Be(3);
            viewModel.StateProvinces.ToList()[0].Text.Should().Be("--Select--");
        }

        [Fact]
        public async void UpdateAddress_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();

            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();

            var mockStateProvinceService = new Mock<IStateProvinceService>();
            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = new EditCustomerAddressViewModel
            {

            };
            await svc.UpdateAddress(viewModel);

            //Assert
            mockCustomerService.Verify(x => x.UpdateCustomerAddressAsync(It.IsAny<UpdateCustomerAddress.UpdateCustomerAddressRequest>()));
        }

        [Fact]
        public async void GetCustomerAddressForDelete_Store_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();

            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();

            var mockCustomerService = new Mock<ICustomerApi>();
            mockCustomerService.Setup(x => x.GetCustomerAsync(It.IsAny<GetCustomer.GetCustomerRequest>()))
                .ReturnsAsync(new GetCustomer.GetCustomerResponse
                {
                    Customer = new CustomerBuilder()
                        .Store(new StoreBuilder()
                            .Name("A Bike Store")
                            .Addresses(new CustomerAddressBuilder()
                                .AddressTypeName("Main Office")
                                .Address(new AddressBuilder()
                                    .StateProvince(new StateProvinceBuilder()
                                        .StateProvinceCode("AZ")
                                        .Name("Arizona")
                                        .CountryRegion(new CountryRegionBuilder()
                                            .CountryRegionCode("US")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                                )
                                .Build()
                            )
                            .Build()
                        )
                        .Build()
                });
            
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();

            var mockStateProvinceService = new Mock<IStateProvinceService>();
            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
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
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();

            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();

            var mockCustomerService = new Mock<ICustomerApi>();
            mockCustomerService
                .Setup(x => x.GetCustomerAsync(It.IsAny<GetCustomer.GetCustomerRequest>()))
                .ReturnsAsync(new GetCustomer.GetCustomerResponse
                {
                    Customer = new CustomerBuilder()
                        .Person(new PersonBuilder()
                            .FullName("Jon V Yang")
                            .Addresses(new CustomerAddressBuilder()
                                .AddressTypeName("Home")
                                .Address(new AddressBuilder()
                                    .StateProvince(new StateProvinceBuilder()
                                        .StateProvinceCode("AZ")
                                        .Name("Arizona")
                                        .CountryRegion(new CountryRegionBuilder()
                                            .CountryRegionCode("US")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                                )
                                .Build()
                            )
                            .Build()
                        )
                        .Build()
                });

            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();

            var mockStateProvinceService = new Mock<IStateProvinceService>();
            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
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
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();

            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();
            var mockStateProvinceService = new Mock<IStateProvinceService>();
            mockStateProvinceService.Setup(x => x.ListStateProvincesAsync(It.IsAny<ListStateProvincesRequest>()))
                .ReturnsAsync(new ListStateProvincesResponse
                {
                    StateProvinces = new StateProvince[]
                    {
                        new StateProvince { CountryRegionCode = "US", StateProvinceCode = "AZ", Name = "Arizona" },
                        new StateProvince { CountryRegionCode = "US", StateProvinceCode = "CA", Name = "California" }
                    }
                });

            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
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
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();
            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();
            var mockStateProvinceService = new Mock<IStateProvinceService>();

            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            await svc.DeleteAddress("AW00000001", "Main Office");

            //Assert
            mockCustomerService.Verify(x => x.DeleteCustomerAddressAsync(It.IsAny<DeleteCustomerAddress.DeleteCustomerAddressRequest>()));
        }

        [Fact]
        public async void AddContact_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();
            var mockAddressTypeService = new Mock<IAddressTypeService>();
            
            var mockContactTypeService = new Mock<IContactTypeService>();
            mockContactTypeService.Setup(x => x.ListContactTypesAsync())
                .ReturnsAsync(new ListContactTypesResponse
                {
                    ContactTypes = new string[]
                    {
                        "Owner", "Marketing Assistant", "Order Administrator"
                    }
                });

            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();
            var mockStateProvinceService = new Mock<IStateProvinceService>();

            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
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
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();
            var mockAddressTypeService = new Mock<IAddressTypeService>();

            var mockContactTypeService = new Mock<IContactTypeService>();
            mockContactTypeService.Setup(x => x.ListContactTypesAsync())
                .ReturnsAsync(new ListContactTypesResponse
                {
                    ContactTypes = new string[]
                    {
                        "Owner", "Marketing Assistant", "Order Administrator"
                    }
                });

            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();
            var mockStateProvinceService = new Mock<IStateProvinceService>();

            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = new EditCustomerContactViewModel();
            await svc.AddContact(viewModel);

            //Assert
            mockCustomerService.Verify(x => x.AddCustomerContactAsync(It.IsAny<AddCustomerContact.AddCustomerContactRequest>()));
        }

        [Fact]
        public async void GetCustomerContact_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();
            var mockAddressTypeService = new Mock<IAddressTypeService>();
            
            var mockContactTypeService = new Mock<IContactTypeService>();
            mockContactTypeService.Setup(x => x.ListContactTypesAsync())
                .ReturnsAsync(new ListContactTypesResponse
                {
                    ContactTypes = new string[] { "Owner", "Order Administrator", "Product Manager" }
                });

            var mockCountryService = new Mock<ICountryService>();
            mockCountryService.Setup(x => x.ListCountriesAsync())
                .ReturnsAsync(new ListCountriesResponse
                {
                    Countries = new CountryDto[]
                    {
                        new CountryDto { CountryRegionCode = "US", Name = "United States"},
                        new CountryDto { CountryRegionCode = "GB", Name = "United Kingdom"}
                    }
                });

            var mockCustomerService = new Mock<ICustomerApi>();
            mockCustomerService
                .Setup(x => x.GetCustomerAsync(It.IsAny<GetCustomer.GetCustomerRequest>()))
                .ReturnsAsync(new GetCustomer.GetCustomerResponse
                {
                    Customer = new CustomerBuilder()
                        .Store(new StoreBuilder()
                            .Name("A Bike Store")
                            .Contacts(new CustomerContactBuilder()
                                .ContactTypeName("Order Administrator")
                                .Contact(new ContactBuilder()
                                    .FullName("Orlando N. Gee")
                                    .Build()
                                )
                                .Build()
                            )
                            .Build()
                        )
                        .Build()
                });

            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();
            var mockStateProvinceService = new Mock<IStateProvinceService>();

            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
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
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();

            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();

            var mockStateProvinceService = new Mock<IStateProvinceService>();
            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = new EditCustomerContactViewModel
            {

            };
            await svc.UpdateContact(viewModel);

            //Assert
            mockCustomerService.Verify(x => x.UpdateCustomerContactAsync(It.IsAny<UpdateCustomerContact.UpdateCustomerContactRequest>()));
        }

        [Fact]
        public async void GetCustomerContactForDelete_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();

            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();

            var mockCustomerService = new Mock<ICustomerApi>();
            mockCustomerService.Setup(x => x.GetCustomerAsync(It.IsAny<GetCustomer.GetCustomerRequest>()))
                .ReturnsAsync(new GetCustomer.GetCustomerResponse
                {
                    Customer = new CustomerBuilder()
                        .Store(new StoreBuilder()
                            .Name("A Bike Store")
                            .Contacts(new CustomerContactBuilder()
                                .ContactTypeName("Order Administrator")
                                .Contact(new ContactBuilder()
                                    .FullName("Orlando N. Gee")
                                    .Build()
                                )
                                .Build()
                            )
                            .Build()
                        )
                        .Build()
                });

            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();

            var mockStateProvinceService = new Mock<IStateProvinceService>();
            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = await svc.GetCustomerContactForDelete("AW00000001", "Orlando N. Gee", "Order Administrator");

            //Assert
            viewModel.AccountNumber.Should().Be("AW00000001");
            viewModel.CustomerName.Should().Be("A Bike Store");
            viewModel.ContactType.Should().Be("Order Administrator");
        }

        [Fact]
        public async void DeleteContact_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();

            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();

            var mockStateProvinceService = new Mock<IStateProvinceService>();
            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = new DeleteCustomerContactViewModel();
            await svc.DeleteContact(viewModel);

            //Assert
            mockCustomerService.Verify(x => x.DeleteCustomerContactAsync(It.IsAny<DeleteCustomerContact.DeleteCustomerContactRequest>()));
        }

        [Fact]
        public async void AddContactInformation_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();

            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            mockContactTypeService.Setup(x => x.ListContactTypesAsync())
                .ReturnsAsync(new ListContactTypesResponse
                {
                    ContactTypes = new string[] { "Owner", "Order Administrator", "Product Manager" }
                });

            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();

            var mockStateProvinceService = new Mock<IStateProvinceService>();
            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = await svc.AddContactInformation("AW00000001", "A Bike Store");

            //Assert
            viewModel.IsNewContactInfo.Should().Be(true);
            viewModel.ChannelTypes.Count().Should().Be(3);
            viewModel.ChannelTypes.ToList()[0].Text.Should().Be("--Select--");
            viewModel.ContactInfoTypes.Count().Should().Be(4);
        }

        [Fact]
        public async void AddContactInformation_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();

            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();

            var mockStateProvinceService = new Mock<IStateProvinceService>();
            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = new EditCustomerContactInfoViewModel();
            await svc.AddContactInformation(viewModel);

            //Assert
            mockCustomerService.Verify(x => x.AddCustomerContactInfoAsync(It.IsAny<AddCustomerContactInfo.AddCustomerContactInfoRequest>()));
        }

        [Fact]
        public async void GetCustomerContactInformationForDelete_ReturnsViewModel()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();

            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();

            var mockCustomerService = new Mock<ICustomerApi>();
            mockCustomerService.Setup(x => x.GetCustomerAsync(It.IsAny<GetCustomer.GetCustomerRequest>()))
                .ReturnsAsync(new GetCustomer.GetCustomerResponse
                {
                    Customer = new CustomerBuilder()
                        .Person(new PersonBuilder()
                            .FullName("Jon V Yang")
                            .ContactInfo(new ContactInfoBuilder()
                                .ContactInfoChannelType(Core.Domain.Person.ContactInfoChannelType.Email)
                                .Value("orlando0@adventure-works.com")
                                .Build()
                            )
                            .Build()
                        )
                        .Build()
                });

            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();

            var mockStateProvinceService = new Mock<IStateProvinceService>();
            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = await svc.GetCustomerContactInformationForDelete("AW00000001", ContactInfoChannelTypeViewModel.Email, "orlando0@adventure-works.com");

            //Assert
            viewModel.AccountNumber.Should().Be("AW00000001");
            viewModel.CustomerName.Should().Be("Jon V Yang");
            viewModel.CustomerContactInfo.Channel.Should().Be(Core.Domain.Person.ContactInfoChannelType.Email);
            viewModel.CustomerContactInfo.Value.Should().Be("orlando0@adventure-works.com");
        }

        [Fact]
        public async void DeleteContactInformation_OK()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CustomerViewModelService>>();

            var mockAddressTypeService = new Mock<IAddressTypeService>();
            var mockContactTypeService = new Mock<IContactTypeService>();
            var mockCountryService = new Mock<ICountryService>();
            var mockCustomerService = new Mock<ICustomerApi>();
            var mockSalesTerritoryService = new Mock<ISalesTerritoryService>();
            var mockSalesPersonService = new Mock<ISalesPersonService>();

            var mockStateProvinceService = new Mock<IStateProvinceService>();
            var svc = new CustomerViewModelService(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockAddressTypeService.Object,
                mockContactTypeService.Object,
                mockCountryService.Object,
                mockCustomerService.Object,
                mockSalesTerritoryService.Object,
                mockSalesPersonService.Object,
                mockStateProvinceService.Object
            );

            //Act
            var viewModel = new DeleteCustomerContactInfoViewModel();
            await svc.DeleteContactInformation(viewModel);

            //Assert
            mockCustomerService.Verify(x => x.DeleteCustomerContactInfoAsync(It.IsAny<DeleteCustomerContactInfo.DeleteCustomerContactInfoRequest>()));
        }
    }
}