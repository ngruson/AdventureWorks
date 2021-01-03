using Ardalis.GuardClauses;
using AutoMapper;
using AW.Core.Domain.Sales;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.ViewModels;
using AW.UI.Web.Internal.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AW.Core.Abstractions.Api.CustomerApi;
using AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerAddress;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerContact;
using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerContact;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContact;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerContactInfo;
using AW.Core.Domain.Person;
using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContactInfo;
using AW.Core.Abstractions.Api.AddressTypeApi;
using AW.Core.Abstractions.Api.ContactTypeApi;
using AW.Core.Abstractions.Api.CountryApi;
using AW.Core.Abstractions.Api.StateProvinceApi;
using AW.Core.Abstractions.Api.SalesTerritoryApi;
using AW.Core.Abstractions.Api.SalesPersonApi;
using AW.Core.Abstractions.Api.SalesPersonApi.ListSalesPersons;
using AW.Core.Abstractions.Api.StateProvinceApi.ListStateProvinces;
using AW.Core.Abstractions.Api.SalesPersonApi.GetSalesPerson;

namespace AW.UI.Web.Internal.Services
{
    public class CustomerViewModelService : ICustomerViewModelService
    {
        private readonly ILogger<CustomerViewModelService> logger;
        private readonly IMapper mapper;

        private readonly IAddressTypeApi addressTypeApi;
        private readonly IContactTypeApi contactTypeApi;
        private readonly ICountryApi countryApi;
        private readonly ICustomerApi customerApi;
        private readonly ISalesTerritoryApi salesTerritoryApi;
        private readonly ISalesPersonApi salesPersonApi;
        private readonly IStateProvinceApi stateProvinceApi;

        public CustomerViewModelService(
            ILogger<CustomerViewModelService> logger,
            IMapper mapper,
            IAddressTypeApi addressTypeApi,
            IContactTypeApi contactTypeApi,
            ICountryApi countryApi,
            ICustomerApi customerApi,
            ISalesTerritoryApi salesTerritoryApi,
            ISalesPersonApi salesPersonApi,
            IStateProvinceApi stateProvinceApi)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.addressTypeApi = addressTypeApi;
            this.contactTypeApi = contactTypeApi;
            this.countryApi = countryApi;
            this.customerApi = customerApi;
            this.salesTerritoryApi = salesTerritoryApi;
            this.salesPersonApi = salesPersonApi;
            this.stateProvinceApi = stateProvinceApi;
        }

        public async Task<CustomersIndexViewModel> GetCustomers(int pageIndex, int pageSize, string territory, string customerType)
        {
            logger.LogInformation("GetCustomers called");

            var response = await customerApi.ListCustomersAsync(
                new ListCustomersRequest
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Territory = territory,
                    CustomerType = !string.IsNullOrEmpty(customerType) ? Enum.Parse<CustomerType>(customerType) : default
                }
            );

            var vm = new CustomersIndexViewModel
            {
                Customers = mapper.Map<List<CustomerViewModel>>(response.Customers),
                Territories = await GetTerritories(false),
                CustomerTypes = GetCustomerTypes(),
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = response.Customers.Count,
                    TotalItems = response.TotalCustomers,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)response.TotalCustomers / pageSize)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "disabled" : "";

            return vm;
        }

        public async Task<CustomerDetailViewModel> GetCustomer(string accountNumber)
        {
            logger.LogInformation("GetCustomer called");

            var response = await customerApi.GetCustomerAsync(
                new GetCustomerRequest
                {
                    AccountNumber = accountNumber
                }
            );

            var vm = new CustomerDetailViewModel
            {
                Customer = mapper.Map<CustomerViewModel>(response.Customer),
            };

            return vm;
        }

        public async Task<EditStoreCustomerViewModel> GetStoreCustomerForEdit(string accountNumber)
        {
            logger.LogInformation("GetStoreCustomerForEdit called");

            var response = await customerApi.GetCustomerAsync(
                new GetCustomerRequest
                {
                    AccountNumber = accountNumber
                }
            );

            var vm = new EditStoreCustomerViewModel
            {
                Customer = mapper.Map<CustomerViewModel>(response.Customer),
                Territories = await GetTerritories(true),
                SalesPersons = await GetSalesPersons(response.Customer.SalesTerritoryName)
            };

            return vm;
        }

        public async Task<EditIndividualCustomerViewModel> GetIndividualCustomerForEdit(string accountNumber)
        {
            logger.LogInformation("GetIndividualCustomerForEdit called");

            var response = await customerApi.GetCustomerAsync(
                new GetCustomerRequest
                {
                    AccountNumber = accountNumber
                }
            );            

            var vm = new EditIndividualCustomerViewModel
            {
                Customer = mapper.Map<CustomerViewModel>(response.Customer),
                Territories = await GetTerritories(true),
                EmailPromotions = GetEmailPromotions()
            };

            return vm;
        }        

        private async Task<IEnumerable<SelectListItem>> GetTerritories(bool edit)
        {
            logger.LogInformation("GetTerritories called.");
            var territories = await salesTerritoryApi.ListTerritoriesAsync();

            var items = territories
                .Territories
                .Select(t => new SelectListItem() { Value = t.Name, Text = $"{t.Name} ({t.CountryRegion.CountryRegionCode})" })
                .OrderBy(b => b.Text)
                .ToList();

            if (edit)
                items.Insert(0, new SelectListItem { Value = "", Text = "--Select--", Selected = true });
            else
                items.Insert(0, new SelectListItem { Value = "", Text = "All", Selected = true });

            return items;
        }

        private IEnumerable<SelectListItem> GetEmailPromotions()
        {
            var items = new List<SelectListItem>();

            foreach (EmailPromotionViewModel emailPromotion in (EmailPromotionViewModel[])Enum.GetValues(typeof(EmailPromotionViewModel)))
            {
                items.Add(new SelectListItem(
                        EnumHelper<EmailPromotionViewModel>.GetDisplayValue(emailPromotion),
                        emailPromotion.ToString()
                    )
                );
            }

            var allItem = new SelectListItem() { Value = "", Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        private List<SelectListItem> GetCustomerTypes()
        {
            logger.LogInformation("GetCustomerTypes called.");

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = "", Text = "All", Selected = true },
                new SelectListItem() { Value = "Individual", Text = "Individual"},
                new SelectListItem() { Value = "Store", Text = "Store" }
            };

            return items;
        }

        private async Task<IEnumerable<SelectListItem>> GetSalesPersons(string salesTerritoryName)
        {
            logger.LogInformation("GetSalesPersons called.");
            var salesPersons = await salesPersonApi.ListSalesPersonsAsync(
                new ListSalesPersonsRequest
                {
                    Territory = salesTerritoryName
                }
            );

            var items = salesPersons
                .SalesPersons
                .Select(t => new SelectListItem() { Value = t.FullName, Text = t.FullName })
                .OrderBy(b => b.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        public async Task UpdateStore(CustomerViewModel viewModel)
        {
            logger.LogInformation("UpdateStore called");
            Guard.Against.Null(viewModel.Store, nameof(viewModel.Store));

            if (!string.IsNullOrEmpty(viewModel.Store.SalesPerson?.FullName))
            {
                var salesPersonResponse = await salesPersonApi.GetSalesPersonAsync(new GetSalesPersonRequest 
                    { FullName = viewModel.Store.SalesPerson.FullName }
                );

                viewModel.Store.SalesPerson = mapper.Map<SalesPersonViewModel>(salesPersonResponse.SalesPerson);
            }

            logger.LogInformation("Mapping CustomerViewModel to UpdateCustomerRequest");
            var request = mapper.Map<UpdateCustomerRequest>(viewModel);
                
            logger.LogInformation("Calling UpdateCustomer operation of Customer web service");
            await customerApi.UpdateCustomerAsync(request);
            logger.LogInformation("Customer successfully updated");
        }

        public async Task UpdateIndividual(CustomerViewModel viewModel)
        {
            logger.LogInformation("UpdateIndividual called");
            logger.LogInformation("Mapping CustomerViewModel to UpdateCustomerRequest");

            var request = mapper.Map<UpdateCustomerRequest>(viewModel);

            logger.LogInformation("Calling UpdateCustomer operation of Customer web service");
            await customerApi.UpdateCustomerAsync(request);
            logger.LogInformation("Customer successfully updated");
        }

        public async Task<EditCustomerAddressViewModel> AddAddress(string accountNumber, string customerName)
        {
            logger.LogInformation("AddAddress called");

            var vm = new EditCustomerAddressViewModel
            {
                IsNewAddress = true,
                AccountNumber = accountNumber,
                CustomerName = customerName,
                CustomerAddress = new CustomerAddressViewModel {  
                    Address = new AddressViewModel
                    {
                        StateProvince = new StateProvinceViewModel
                        {
                            CountryRegion = new CountryRegionViewModel
                            {
                                CountryRegionCode = "US"
                            }
                        }
                    }
                },
                AddressTypes = await GetAddressTypes(),
                Countries = await GetCountries(),
                StateProvinces = await GetStateProvinces("US")
            };

            return vm;
        }

        public async Task AddAddress(EditCustomerAddressViewModel viewModel)
        {
            logger.LogInformation("AddAddress called");

            var request = mapper.Map<AddCustomerAddressRequest>(viewModel);

            logger.LogInformation("Calling AddCustomerAddress operation of Customer web service");
            await customerApi.AddCustomerAddressAsync(request);
            logger.LogInformation("Address successfully added");
        }

        private async Task<IEnumerable<SelectListItem>> GetAddressTypes()
        {
            logger.LogInformation("GetAddressTypes called.");
            var addressTypes = await addressTypeApi.ListAddressTypesAsync();

            var items = addressTypes
                .AddressTypes
                .Select(at => new SelectListItem() { Value = at, Text = at })
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        private async Task<IEnumerable<SelectListItem>> GetCountries()
        {
            logger.LogInformation("GetCountries called.");
            var response = await countryApi.ListCountriesAsync();

            var items = response     
                .Countries
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem() { Value = c.CountryRegionCode, Text = c.Name })                
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        public async Task<EditCustomerAddressViewModel> GetCustomerAddress(string accountNumber, string addressType)
        {
            logger.LogInformation("GetCustomerAddress called");

            var response = await customerApi.GetCustomerAsync(
                new GetCustomerRequest
                {
                    AccountNumber = accountNumber
                }
            );

            var addresses = response.Customer.Store != null ? response.Customer.Store.Addresses :
                response.Customer.Person.Addresses;

            var address = addresses.FirstOrDefault(a => a.AddressType == addressType);

            var vm = new EditCustomerAddressViewModel
            {
                AccountNumber = accountNumber,
                CustomerName = response.Customer.Store != null ? response.Customer.Store.Name : response.Customer.Person.FullName,
                CustomerAddress = mapper.Map<CustomerAddressViewModel>(address),
                AddressTypes = await GetAddressTypes(),
                Countries = await GetCountries(),
                StateProvinces = await GetStateProvinces(address.Address.StateProvince.CountryRegion.CountryRegionCode)
            };

            return vm;
        }

        public async Task UpdateAddress(EditCustomerAddressViewModel viewModel)
        {
            logger.LogInformation("UpdateAddress called");

            var request = mapper.Map<UpdateCustomerAddressRequest>(viewModel);

            logger.LogInformation("Calling UpdateCustomerAddress operation of Customer web service");
            await customerApi.UpdateCustomerAddressAsync(request);
            logger.LogInformation("Address successfully updated");
        }

        public async Task<IEnumerable<SelectListItem>> GetStateProvinces(string country)
        {
            logger.LogInformation("GetStateProvinces called.");
            var response = await stateProvinceApi.ListStateProvincesAsync(new ListStateProvincesRequest
            {
                CountryRegionCode = country
            });

            var items = response
                .StateProvinces
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem() { Value = c.StateProvinceCode, Text = c.Name })
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        public async Task<DeleteCustomerAddressViewModel> GetCustomerAddressForDelete(string accountNumber, string addressType)
        {
            logger.LogInformation("GetCustomerAddressForDelete called");

            var response = await customerApi.GetCustomerAsync(
                new GetCustomerRequest
                {
                    AccountNumber = accountNumber
                }
            );

            var addresses = response.Customer.Store != null ? response.Customer.Store.Addresses :
                response.Customer.Person.Addresses;

            var address = addresses.FirstOrDefault(a => a.AddressType == addressType);

            var vm = mapper.Map<DeleteCustomerAddressViewModel>(address);
            vm.AccountNumber = accountNumber;
            vm.CustomerName = response.Customer.Store != null ? response.Customer.Store.Name : response.Customer.Person.FullName;

            return vm;
        }

        public async Task<IEnumerable<StateProvinceViewModel>> GetStateProvincesJson(string country)
        {
            var response = await stateProvinceApi.ListStateProvincesAsync(new ListStateProvincesRequest
            {
                CountryRegionCode = country
            });

            return mapper.Map<IEnumerable<StateProvinceViewModel>>(response.StateProvinces);
        }

        public async Task DeleteAddress(string accountNumber, string addressType)
        {
            logger.LogInformation("DeleteAddress called");

            logger.LogInformation("Calling DeleteCustomerAddress operation of Customer web service");
            await customerApi.DeleteCustomerAddressAsync(new DeleteCustomerAddressRequest
            {
                AccountNumber = accountNumber,
                AddressType = addressType
            });
            logger.LogInformation("Address successfully deleted");
        }

        public async Task<EditCustomerContactViewModel> AddContact(string accountNumber, string customerName)
        {
            logger.LogInformation("AddContact called");

            var vm = new EditCustomerContactViewModel
            {
                IsNewContact = true,
                AccountNumber = accountNumber,
                CustomerName = customerName,
                CustomerContact = new CustomerContactViewModel
                {
                    Contact = new ContactViewModel
                    {
                    }
                },
                ContactTypes = await GetContactTypes()
            };

            return vm;
        }

        public async Task AddContact(EditCustomerContactViewModel viewModel)
        {
            logger.LogInformation("AddContact called");

            var request = mapper.Map<AddCustomerContactRequest>(viewModel);

            logger.LogInformation("Calling AddCustomerContact operation of Customer web service");
            await customerApi.AddCustomerContactAsync(request);
            logger.LogInformation("Contact successfully added");
        }

        private async Task<IEnumerable<SelectListItem>> GetContactTypes()
        {
            logger.LogInformation("GetContactTypes called.");
            var contactTypes = await contactTypeApi.ListContactTypesAsync();

            var items = contactTypes
                .ContactTypes
                .Select(at => new SelectListItem() { Value = at, Text = at })
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        public async Task<EditCustomerContactViewModel> GetCustomerContact(string accountNumber, string contactName, string contactType)
        {
            logger.LogInformation("GetCustomerContact called");

            var response = await customerApi.GetCustomerAsync(
                new GetCustomerRequest
                {
                    AccountNumber = accountNumber
                }
            );

            var contact = response.Customer.Store.Contacts.FirstOrDefault(c => 
                c.ContactType == contactType &&
                c.Contact.FullName == contactName
            );

            var vm = new EditCustomerContactViewModel
            {
                IsNewContact = false,
                AccountNumber = accountNumber,
                CustomerName = response.Customer.Store.Name,
                CustomerContact = mapper.Map<CustomerContactViewModel>(contact),
                ContactTypes = await GetContactTypes()
            };

            return vm;
        }

        public async Task UpdateContact(EditCustomerContactViewModel viewModel)
        {
            logger.LogInformation("UpdateContact called");

            var request = mapper.Map<UpdateCustomerContactRequest>(viewModel);

            logger.LogInformation("Calling UpdateCustomerContact operation of Customer web service");
            await customerApi.UpdateCustomerContactAsync(request);
            logger.LogInformation("Contact successfully updated");
        }

        public async Task<DeleteCustomerContactViewModel> GetCustomerContactForDelete(string accountNumber, string contactName, string contactType)
        {
            logger.LogInformation("GetCustomerContactForDelete called");

            var response = await customerApi.GetCustomerAsync(
                new GetCustomerRequest
                {
                    AccountNumber = accountNumber
                }
            );

            var contact = response.Customer.Store.Contacts.FirstOrDefault(a => 
                a.ContactType == contactType && a.Contact.FullName == contactName
            );

            var vm = mapper.Map<DeleteCustomerContactViewModel>(contact);
            vm.AccountNumber = accountNumber;
            vm.CustomerName = response.Customer.Store.Name;

            return vm;
        }

        public async Task DeleteContact(DeleteCustomerContactViewModel viewModel)
        {
            logger.LogInformation("DeleteContact called");

            logger.LogInformation("Calling DeleteCustomerContact operation of Customer web service");
            await customerApi.DeleteCustomerContactAsync(
                mapper.Map<DeleteCustomerContactRequest>(viewModel)
            );

            logger.LogInformation("Contact successfully deleted");
        }

        public async Task<EditCustomerContactInfoViewModel> AddContactInformation(string accountNumber, string customerName)
        {
            logger.LogInformation("AddContactInformation called");

            var vm = new EditCustomerContactInfoViewModel
            {
                IsNewContactInfo = true,
                AccountNumber = accountNumber,
                CustomerName = customerName,
                CustomerContactInfo = new CustomerContactInfoViewModel(),
                ChannelTypes = GetContactInfoChannelTypes(),
                ContactInfoTypes = await GetContactTypes()
            };

            return vm;
        }

        public async Task AddContactInformation(EditCustomerContactInfoViewModel viewModel)
        {
            logger.LogInformation("AddContactInformation called");

            var request = mapper.Map<AddCustomerContactInfoRequest>(viewModel);

            logger.LogInformation("Calling AddCustomerContactInfo operation of Customer web service");
            await customerApi.AddCustomerContactInfoAsync(request);
            logger.LogInformation("Contact successfully added");
        }

        private IEnumerable<SelectListItem> GetContactInfoChannelTypes()
        {
            logger.LogInformation("GetContactInfoChannelTypes called.");

            var contactInfoTypes = EnumHelper<ContactInfoChannelTypeViewModel>.GetValues();

            var items = contactInfoTypes
                .Select(c => new SelectListItem
                {
                    Value = c.ToString(),
                    Text = EnumHelper<ContactInfoChannelTypeViewModel>.GetDisplayValue(c)
                })
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }        

        public async Task<DeleteCustomerContactInfoViewModel> GetCustomerContactInformationForDelete(string accountNumber, ContactInfoChannelTypeViewModel channel, string value)
        {
            logger.LogInformation("GetCustomerContactInformationForDelete called");

            var response = await customerApi.GetCustomerAsync(
                new GetCustomerRequest
                {
                    AccountNumber = accountNumber
                }
            );

            var channelType = Enum.Parse<ContactInfoChannelType>(channel.ToString());
            var contactInfo = response.Customer.Person.ContactInfo.FirstOrDefault(c =>
                c.Channel == channelType &&
                c.Value == value
            );

            var vm = new DeleteCustomerContactInfoViewModel
            {
                AccountNumber = accountNumber,
                CustomerName = response.Customer.Person.FullName,
                CustomerContactInfo = mapper.Map<CustomerContactInfoViewModel>(contactInfo)
            };

            return vm;
        }

        public async Task DeleteContactInformation(DeleteCustomerContactInfoViewModel viewModel)
        {
            logger.LogInformation("DeleteContactInformation called");

            logger.LogInformation("Calling DeleteCustomerContact operation of Customer web service");
            await customerApi.DeleteCustomerContactInfoAsync(
                mapper.Map<DeleteCustomerContactInfoRequest>(viewModel)
            );

            logger.LogInformation("Contact information successfully deleted");
        }
    }
}