using FluentAssertions;
using Moq;
using System.Linq;
using Xunit;
using System.Collections.Generic;
using AW.UI.Web.Internal.UnitTests.TestBuilders.GetCustomer;
using customerApi = AW.UI.Web.Infrastructure.ApiClients.CustomerApi;
using AW.UI.Web.Internal.Services;
using Microsoft.Extensions.Logging;
using AW.UI.Web.Internal.ViewModels.Customer;
using AW.SharedKernel.Interfaces;
using System.Threading.Tasks;
using AW.SharedKernel.UnitTesting;
using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using MediatR;
using System.Threading;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetStatesProvinces;
using AW.UI.Web.Infrastructure.ApiClients.CustomerApi;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetAddressTypes;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetContactTypes;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetCountries;
using AW.UI.Web.SharedKernel.SalesPerson.Handlers.GetSalesPersons;

namespace AW.UI.Web.Internal.UnitTests.Services
{
    public class CustomerServiceUnitTests
    {
        public class GetCustomers
        {            
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetCustomers_FirstPage_ReturnsViewModel(
                [Frozen] Mock<IMediator> mockMediator,
                [Frozen] Mock<customerApi.ICustomerApiClient> mockCustomerApi,
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

                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetTerritoriesQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
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
                [Frozen] Mock<IMediator> mockMediator,
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

                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetTerritoriesQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
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

        public class GetCustomer
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetCustomer_ReturnsViewModel(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                customerApi.Models.GetCustomer.StoreCustomer customer
            )
            {
                //Arrange
                mockCustomerApiClient.Setup(_ => _.GetCustomerAsync(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(customer);

                //Act
                var viewModel = await sut.GetCustomer(customer.AccountNumber);

                //Assert
                viewModel.Customer.AccountNumber.Should().Be(customer.AccountNumber);
            }
        }

        public class GetStoreCustomerForEdit
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetStoreCustomerForEdit_ReturnsViewModel(
                [Frozen] Mock<IMediator> mockMediator,
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                customerApi.Models.GetCustomer.StoreCustomer customer,
                List<Territory> territories,
                List<SalesPerson> salesPersons
            )
            {
                //Arrange
                mockCustomerApiClient.Setup(x => x
                    .GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(
                        It.IsAny<string>()
                ))
               .ReturnsAsync(customer);

                mockMediator.Setup(x => x.Send(
                        It.IsAny<GetTerritoriesQuery>(),                    
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(territories);

                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetSalesPersonsQuery>(),                    
                        It.IsAny<CancellationToken>()
                    )
                )
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

        public class GetIndividualCustomerForEdit
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetIndividualCustomerForEdit_ReturnsViewModel(
                [Frozen] Mock<IMediator> mockMediator,
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                List<Territory> territories,
                CustomerService sut,
                customerApi.Models.GetCustomer.IndividualCustomer customer
            )
            {
                //Arrange
                mockCustomerApiClient.Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.IndividualCustomer>(
                        It.IsAny<string>()
                    )
                )
               .ReturnsAsync(customer);

                mockMediator.Setup(x => x.Send(
                        It.IsAny<GetTerritoriesQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(territories);

                //Act
                var viewModel = await sut.GetIndividualCustomerForEdit(customer.AccountNumber);

                //Assert
                viewModel.Customer.AccountNumber.Should().Be(customer.AccountNumber);
                viewModel.Territories.ToList().Count.Should().Be(4);
                viewModel.Territories.ToList()[0].Text.Should().Be("--Select--");
                viewModel.EmailPromotions.Count().Should().Be(4);
                viewModel.EmailPromotions.ToList()[0].Text.Should().Be("All");
            }
        }

        public class UpdateStore
        {
            [Theory, AutoMoqData]
            public async Task UpdateStore_ReturnsViewModel(
                [Frozen] Mock<customerApi.ICustomerApiClient> mockCustomerApi,
                CustomerService sut,
                StoreCustomerViewModel viewModel,
                customerApi.Models.GetCustomer.StoreCustomer customer
            )
            {
                //Arrange
                mockCustomerApi.Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(
                        It.IsAny<string>()
                    )
                )
               .ReturnsAsync(customer);

                //Act
                await sut.UpdateStore(viewModel);

                //Assert
                mockCustomerApi.Verify(x => x.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<customerApi.Models.UpdateCustomer.Customer>())
                );
            }

            [Theory, AutoMoqData]
            public async Task UpdateStore_OK(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                StoreCustomerViewModel viewModel,
                customerApi.Models.GetCustomer.StoreCustomer customer
            )
            {
                //Arrange
                mockCustomerApiClient.Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(
                        It.IsAny<string>()
                    )
                )
               .ReturnsAsync(customer);

                //Act
                await sut.UpdateStore(viewModel);

                //Assert
                mockCustomerApiClient.Verify(x => x.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<customerApi.Models.UpdateCustomer.Customer>())
                );
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task UpdateStore_WithSalesPerson_OK(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                StoreCustomerViewModel viewModel,
                customerApi.Models.GetCustomer.StoreCustomer customer
            )
            {
                //Arrange
                mockCustomerApiClient.Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(
                        It.IsAny<string>()
                    )
                )
               .ReturnsAsync(customer);

                //Act
                await sut.UpdateStore(viewModel);

                //Assert
                mockCustomerApiClient.Verify(x => x.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<customerApi.Models.UpdateCustomer.Customer>())
                );
            }
        }

        public class UpdateIndividual
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task UpdateIndividual_OK(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                IndividualCustomerViewModel viewModel
            )
            {
                //Arrange

                //Act
                await sut.UpdateIndividual(viewModel);

                //Assert
                mockCustomerApiClient.Verify(x => x.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<customerApi.Models.UpdateCustomer.IndividualCustomer>())
                );
            }
        }

        public class AddAddress
        {
            [Theory, AutoMoqData]
            public void AddAddress_ReturnsViewModel(
                CustomerService sut,
                string accountNumber,
                string customerName
            )
            {
                //Arrange

                //Act
                var viewModel = sut.AddAddress(accountNumber, customerName);

                //Assert
                viewModel.IsNewAddress.Should().Be(true);
                viewModel.AccountNumber.Should().Be(accountNumber);
                viewModel.CustomerName.Should().Be(customerName);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task AddAddress_OK(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                EditCustomerAddressViewModel viewModel,
                customerApi.Models.GetCustomer.StoreCustomer customer
            )
            {
                //Arrange
                mockCustomerApiClient.Setup(x => x.GetCustomerAsync(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(customer);

                //Act
                await sut.AddAddress(viewModel);

                //Assert
                mockCustomerApiClient.Verify(x => x.UpdateCustomerAsync(
                        It.IsAny<string>(),
                        It.IsAny<customerApi.Models.UpdateCustomer.Customer>()
                    )
                );
            }
        }

        public class GetCustomerAddress
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetCustomerAddress_ReturnsViewModel(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                customerApi.Models.GetCustomer.StoreCustomer customer
            )
            {
                //Arrange
                mockCustomerApiClient.Setup(_ => _.GetCustomerAsync(
                    It.IsAny<string>()
                    )
                )
                .ReturnsAsync(customer);

                //Act
                var viewModel = await sut.GetCustomerAddress(
                    customer.AccountNumber,
                    customer.Addresses[0].AddressType
                );

                //Assert
                viewModel.AccountNumber.Should().Be(customer.AccountNumber);
                viewModel.CustomerName.Should().Be(customer.Name);
                viewModel.CustomerAddress.Should().NotBeNull();
            }
        }

        public class UpdateAddress
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task UpdateAddress_OK(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                EditCustomerAddressViewModel viewModel,
                customerApi.Models.GetCustomer.StoreCustomer customer
            )
            {
                //Arrange
                viewModel.CustomerAddress.AddressType = customer.Addresses[0].AddressType;

                mockCustomerApiClient.Setup(x => x.GetCustomerAsync(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(customer);

                //Act
                await sut.UpdateAddress(viewModel);

                //Assert
                mockCustomerApiClient.Verify(x => x.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<customerApi.Models.UpdateCustomer.Customer>()
                ));
            }
        }

        public class GetCustomerAddressForDelete
        {
            [Theory, AutoMoqData]
            public async Task GetCustomerAddressForDelete_Store_ReturnsViewModel(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                customerApi.Models.GetCustomer.StoreCustomer customer
            )
            {
                //Arrange
                mockCustomerApiClient.Setup(x => x.GetCustomerAsync(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(customer);

                //Act
                var viewModel = await sut.GetCustomerAddressForDelete(customer.AccountNumber, "Main Office");

                //Assert
                viewModel.CustomerName.Should().Be(customer.Name);
            }

            [Theory, AutoMoqData]
            public async Task GetCustomerAddressForDelete_Person_ReturnsViewModel(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                customerApi.Models.GetCustomer.IndividualCustomer customer
            )
            {
                //Arrange
                mockCustomerApiClient.Setup(x => x.GetCustomerAsync(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(customer);

                //Act
                var viewModel = await sut.GetCustomerAddressForDelete(customer.AccountNumber, "Home");

                //Assert
                viewModel.CustomerName.Should().Be(customer.CustomerName);
            }
        }

        public class GetStatesProvinces
        {
            [Theory, AutoMoqData]
            public async Task GetStatesProvinces_ReturnsList(
                [Frozen] Mock<IMediator> mockMediator,
                CustomerService sut,
                List<StateProvince> statesProvinces,
                string countryRegionCode
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetStatesProvincesQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(statesProvinces);

                //Act
                var selectListItems = await sut.GetStatesProvinces(countryRegionCode);
                var list = selectListItems.ToList();

                //Assert
                list.Count.Should().Be(4);
                list[0].Value.Should().Be("");
                list[0].Text.Should().Be("--Select--");
            }
        }

        public class GetStatesProvincesJson
        {
            [Theory, AutoMoqData]
            public async Task GetStatesProvincesJson_ReturnsViewModel(
                [Frozen] Mock<IMediator> mockMediator,
                CustomerService sut,
                string countryRegionCode,
                List<StateProvince> statesProvinces
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                        It.IsAny<GetStatesProvincesQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(statesProvinces);

                //Act
                var viewModel = await sut.GetStatesProvincesJson(countryRegionCode);

                //Assert
                viewModel.Should().BeEquivalentTo(statesProvinces);
            }
        }

        public class DeleteAddress
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task DeleteAddress_OK(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                customerApi.Models.GetCustomer.IndividualCustomer customer
            )
            {
                //Arrange
                mockCustomerApiClient
                    .Setup(x => x.GetCustomerAsync(It.IsAny<string>()))
                    .ReturnsAsync(customer);

                //Act
                await sut.DeleteAddress(
                    customer.AccountNumber,
                    customer.Addresses[0].AddressType
                );

                //Assert
                mockCustomerApiClient.Verify(x => x.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<customerApi.Models.UpdateCustomer.Customer>()
                ));
            }
        }

        public class AddContact
        {
            [Theory, AutoMoqData]
            public async Task AddContact_ReturnsViewModel(
                [Frozen] Mock<IMediator> mockMediator,
                CustomerService sut,
                List<ContactType> contactTypes,
                string accountNumber,
                string customerName
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetContactTypesQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(contactTypes);

                //Act
                var viewModel = await sut.AddContact(accountNumber, customerName);

                //Assert
                viewModel.IsNewContact.Should().Be(true);
                viewModel.ContactTypes.Count().Should().Be(4);
                viewModel.ContactTypes.ToList()[0].Text.Should().Be("--Select--");
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task AddContact_OK(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                EditCustomerContactViewModel viewModel,
                customerApi.Models.GetCustomer.StoreCustomer customer
            )
            {
                //Arrange
                mockCustomerApiClient.Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(customer);

                //Act
                await sut.AddContact(viewModel);

                //Assert
                mockCustomerApiClient.Verify(x => x.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<customerApi.Models.UpdateCustomer.Customer>()
                ));
            }
        }

        public class GetCustomerContact
        {
            [Theory, AutoMoqData]
            public async Task GetCustomerContact_ReturnsViewModel(
                [Frozen] Mock<IMediator> mockMediator,
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                List<ContactType> contactTypes,
                customerApi.Models.GetCustomer.StoreCustomer customer,
                string contactName,
                string contactType
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetContactTypesQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(contactTypes);

                mockCustomerApiClient.Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(customer);

                //Act
                var viewModel = await sut.GetCustomerContact(customer.AccountNumber, contactName, contactType);

                //Assert            
                viewModel.IsNewContact.Should().Be(false);
                viewModel.ContactTypes.Count().Should().Be(4);
                viewModel.ContactTypes.ToList()[0].Text.Should().Be("--Select--");
            }
        }

        public class UpdateContact
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task UpdateContact_OK(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                customerApi.Models.GetCustomer.StoreCustomer customer,
                EditCustomerContactViewModel viewModel
            )
            {
                //Arrange
                viewModel.CustomerContact.ContactType = customer.Contacts[0].ContactType;

                mockCustomerApiClient.Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(customer);

                //Act
                await sut.UpdateContact(viewModel);

                //Assert
                mockCustomerApiClient.Verify(x => x.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<customerApi.Models.UpdateCustomer.Customer>()
                ));
            }
        }

        public class GetCustomerContactForDelete
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task GetCustomerContactForDelete_OK(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                customerApi.Models.GetCustomer.StoreCustomer customer
            )
            {
                //Arrange
                mockCustomerApiClient.Setup(x => x.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(customer);

                //Act
                var viewModel = await sut.GetCustomerContactForDelete(
                    customer.AccountNumber,
                    customer.Contacts[0].ContactPerson.Name.FullName,
                    customer.Contacts[0].ContactType
                );

                //Assert
                viewModel.AccountNumber.Should().Be(customer.AccountNumber);
                viewModel.CustomerName.Should().Be(customer.Name);
                viewModel.ContactType.Should().Be(customer.Contacts[0].ContactType);
            }
        }

        public class DeleteContact
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task DeleteContact_OK(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                customerApi.Models.GetCustomer.StoreCustomer customer,
                DeleteCustomerContactViewModel viewModel
            )
            {
                //Arrange
                viewModel.ContactType = customer.Contacts[0].ContactType;

                mockCustomerApiClient.Setup(_ => _.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(customer);

                //Act
                await sut.DeleteContact(viewModel);

                //Assert
                mockCustomerApiClient.Verify(x => x.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<customerApi.Models.UpdateCustomer.Customer>()
                ));
            }
        }

        public class AddContactEmailAddress
        {
            [Theory, AutoMoqData]
            public void AddContactEmailAddress_ReturnsViewModel(
                CustomerService sut,
                string accountNumber,
                string customerName
            )
            {
                //Arrange

                //Act
                var viewModel = sut.AddEmailAddress(accountNumber, customerName);

                //Assert
                viewModel.IsNewEmailAddress.Should().Be(true);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task AddContactEmailAddress_OK(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                EditEmailAddressViewModel viewModel,
                customerApi.Models.GetCustomer.StoreCustomer customer
            )
            {
                //Arrange
                viewModel.PersonName = customer.Contacts[0].ContactPerson.Name.FullName;

                mockCustomerApiClient.Setup(_ => _.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(customer);                

                //Act
                await sut.AddContactEmailAddress(viewModel);

                //Assert
                mockCustomerApiClient.Verify(x => x.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<customerApi.Models.UpdateCustomer.Customer>()
                ));
            }
        }

        public class AddContactPhoneNumber
        {
            [Theory, AutoMoqData]
            public void AddContactPhoneNumber_ReturnsViewModel(
                CustomerService sut,
                string accountNumber,
                string personName
            )
            {
                //Arrange

                //Act
                var viewModel = sut.AddPhoneNumber(accountNumber, personName);

                //Assert
                viewModel.IsNewPhoneNumber.Should().Be(true);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task AddContactPhoneNumber_OK(
                [Frozen] Mock<ICustomerApiClient> mockCustomerApiClient,
                CustomerService sut,
                EditPhoneNumberViewModel viewModel,
                customerApi.Models.GetCustomer.StoreCustomer customer
            )
            {
                //Arrange
                viewModel.PersonName = customer.Contacts[0].ContactPerson.Name.FullName;

                mockCustomerApiClient.Setup(_ => _.GetCustomerAsync<customerApi.Models.GetCustomer.StoreCustomer>(
                        It.IsAny<string>()
                    )
                )
                .ReturnsAsync(customer);

                //Act
                await sut.AddContactPhoneNumber(viewModel);

                //Assert
                mockCustomerApiClient.Verify(x => x.UpdateCustomerAsync(
                    It.IsAny<string>(),
                    It.IsAny<customerApi.Models.UpdateCustomer.Customer>()
                ));
            }
        }
    }
}