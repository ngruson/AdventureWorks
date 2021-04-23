using Ardalis.GuardClauses;
using AutoMapper;
using AW.UI.Web.Internal.ApiClients.CustomerApi;
using AW.UI.Web.Internal.ApiClients.CustomerApi.Models.GetCustomers;
using AW.UI.Web.Internal.ApiClients.ReferenceDataApi;
using AW.UI.Web.Internal.ApiClients.SalesPersonApi;
using AW.UI.Web.Internal.Extensions;
using AW.UI.Web.Internal.ViewModels;
using AW.UI.Web.Internal.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> logger;
        private readonly IMapper mapper;
        private readonly ICustomerApiClient customerApiClient;
        private readonly IReferenceDataApiClient referenceDataApiClient;
        private readonly ISalesPersonApiClient salesPersonApiClient;

        public CustomerService(
            ILogger<CustomerService> logger,
            IMapper mapper,
            ICustomerApiClient customerApiClient,
            IReferenceDataApiClient referenceDataApiClient,
            ISalesPersonApiClient salesPersonApiClient
        )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.customerApiClient = customerApiClient;
            this.referenceDataApiClient = referenceDataApiClient;
            this.salesPersonApiClient = salesPersonApiClient;
        }

        public async Task<CustomersIndexViewModel> GetCustomers(int pageIndex, int pageSize, string territory, CustomerType? customerType, string accountNumber)
        {
            logger.LogInformation("GetCustomers called");
            var response = await customerApiClient.GetCustomersAsync(pageIndex, pageSize, territory, customerType, accountNumber);

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
            var customer = await customerApiClient.GetCustomerAsync(accountNumber);

            var vm = new CustomerDetailViewModel
            {
                Customer = mapper.Map<CustomerViewModel>(customer)
            };

            return vm;
        }

        public async Task<EditStoreCustomerViewModel> GetStoreCustomerForEdit(string accountNumber)
        {
            logger.LogInformation("GetStoreCustomerForEdit called");

            logger.LogInformation("Getting customer for {AccountNumber}", accountNumber);
            var customer = await customerApiClient.GetCustomerAsync<ApiClients.CustomerApi.Models.GetCustomer.StoreCustomer>(
                accountNumber
            );
            logger.LogInformation("Retrieved customer {@Customer}", customer);

            var vm = new EditStoreCustomerViewModel
            {
                Customer = mapper.Map<StoreCustomerViewModel>(customer),
                Territories = await GetTerritories(true),
                SalesPersons = await GetSalesPersons(customer.Territory)
            };

            return vm;
        }

        public async Task<EditIndividualCustomerViewModel> GetIndividualCustomerForEdit(string accountNumber)
        {
            logger.LogInformation("GetIndividualCustomerForEdit called");

            logger.LogInformation("Getting customer for {AccountNumber}", accountNumber);
            var customer = await customerApiClient.GetCustomerAsync<ApiClients.CustomerApi.Models.GetCustomer.IndividualCustomer>(
                accountNumber
            );
            logger.LogInformation("Retrieved customer {@Customer}", customer);

            var vm = new EditIndividualCustomerViewModel
            {
                Customer = mapper.Map<IndividualCustomerViewModel>(customer),
                Territories = await GetTerritories(true),
                EmailPromotions = GetEmailPromotions()
            };

            return vm;
        }

        private async Task<IEnumerable<SelectListItem>> GetTerritories(bool edit)
        {
            logger.LogInformation("GetTerritories called.");
            var territories = await referenceDataApiClient.GetTerritoriesAsync();

            var items = territories
                .Select(t => new SelectListItem() { Value = t.Name, Text = $"{t.Name} ({t.CountryRegionCode})" })
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
            var salesPersons = await salesPersonApiClient.GetSalesPersonsAsync(salesTerritoryName);

            var items = salesPersons
                .Select(t => new SelectListItem() { Value = t.FullName(), Text = t.FullName() })
                .OrderBy(b => b.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        public async Task UpdateStore(StoreCustomerViewModel viewModel)
        {
            logger.LogInformation("UpdateStore called with view model {@ViewModel}", viewModel);
            Guard.Against.Null(viewModel, nameof(viewModel));

            logger.LogInformation("Mapping CustomerViewModel to UpdateCustomerRequest");
            var storeCustomer = await customerApiClient.GetCustomerAsync<ApiClients.CustomerApi.Models.GetCustomer.StoreCustomer>(
                viewModel.AccountNumber
            );
            var storeCustomerToUpdate = mapper.Map<ApiClients.CustomerApi.Models.UpdateCustomer.StoreCustomer>(storeCustomer);

            storeCustomerToUpdate.Name = viewModel.Name;
            storeCustomerToUpdate.Territory = viewModel.Territory;
            storeCustomerToUpdate.SalesPerson = viewModel.SalesPerson;

            logger.LogInformation("Calling Customer API to update customer");
            await customerApiClient.UpdateCustomerAsync(viewModel.AccountNumber, storeCustomerToUpdate);
            logger.LogInformation("Customer successfully updated");
        }

        public async Task UpdateIndividual(IndividualCustomerViewModel viewModel)
        {
            logger.LogInformation("UpdateIndividual called with view model {@ViewModel}", viewModel);
            Guard.Against.Null(viewModel, nameof(viewModel));

            var individualCustomer = mapper.Map<ApiClients.CustomerApi.Models.UpdateCustomer.IndividualCustomer>(viewModel);

            logger.LogInformation("Calling Customer API to update customer");
            await customerApiClient.UpdateCustomerAsync(viewModel.AccountNumber, individualCustomer);
            logger.LogInformation("Customer successfully updated");
        }

        public EditCustomerAddressViewModel AddAddress(string accountNumber, string customerName)
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
                        CountryRegionCode = "US"
                    }
                }
            };

            return vm;
        }

        public async Task AddAddress(EditCustomerAddressViewModel viewModel)
        {
            logger.LogInformation("AddAddress called");

            logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await customerApiClient.GetCustomerAsync(viewModel.AccountNumber);
            logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, nameof(customer));

            var customerToUpdate = mapper.Map<ApiClients.CustomerApi.Models.UpdateCustomer.Customer>(customer);
            Guard.Against.Null(customerToUpdate, nameof(customerToUpdate));
            var newAddress = mapper.Map<ApiClients.CustomerApi.Models.UpdateCustomer.CustomerAddress>(viewModel.CustomerAddress);
            Guard.Against.Null(newAddress, nameof(newAddress));
            customerToUpdate.Addresses.Add(newAddress);

            logger.LogInformation("Updating customer {@Customer}", customer);
            await customerApiClient.UpdateCustomerAsync(viewModel.AccountNumber, customerToUpdate);
            logger.LogInformation("Customer updated successfully");
        }

        public async Task<EditCustomerAddressViewModel> GetCustomerAddress(string accountNumber, string addressType)
        {
            logger.LogInformation("GetCustomerAddress called");
            var customer = await customerApiClient.GetCustomerAsync(accountNumber);
            Guard.Against.Null(customer, nameof(customer));

            var address = customer.Addresses.FirstOrDefault(a => a.AddressType == addressType);
            Guard.Against.Null(address, nameof(address));

            var vm = new EditCustomerAddressViewModel
            {
                AccountNumber = accountNumber,
                CustomerName = customer.CustomerName,
                CustomerAddress = mapper.Map<CustomerAddressViewModel>(address),
            };

            return vm;
        }

        public async Task UpdateAddress(EditCustomerAddressViewModel viewModel)
        {
            logger.LogInformation("UpdateAddress called");

            logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await customerApiClient.GetCustomerAsync(viewModel.AccountNumber);
            logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, nameof(customer));

            var customerToUpdate = mapper.Map<ApiClients.CustomerApi.Models.UpdateCustomer.Customer>(customer);
            Guard.Against.Null(customerToUpdate, nameof(customerToUpdate));
            var addressToUpdate = customerToUpdate.Addresses.FirstOrDefault(a => a.AddressType == viewModel.CustomerAddress.AddressType);
            Guard.Against.Null(addressToUpdate, nameof(addressToUpdate));
            mapper.Map(viewModel.CustomerAddress.Address, addressToUpdate.Address);

            logger.LogInformation("Updating customer {@Customer}", customer);
            await customerApiClient.UpdateCustomerAsync(viewModel.AccountNumber, customerToUpdate);
            logger.LogInformation("Customer updated successfully");
        }

        public async Task<IEnumerable<SelectListItem>> GetStateProvinces(string country)
        {
            logger.LogInformation("GetStateProvinces called.");
            var response = await referenceDataApiClient.GetStateProvincesAsync(country);

            var items = response
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
            var customer = await customerApiClient.GetCustomerAsync(accountNumber);
            var address = customer.Addresses.FirstOrDefault(a => a.AddressType == addressType);

            var vm = mapper.Map<DeleteCustomerAddressViewModel>(address);
            vm.AccountNumber = accountNumber;
            vm.CustomerName = customer.CustomerName;

            return vm;
        }

        public async Task<IEnumerable<StateProvinceViewModel>> GetStateProvincesJson(string country)
        {
            var stateProvinces = await referenceDataApiClient.GetStateProvincesAsync(country);
            return mapper.Map<IEnumerable<StateProvinceViewModel>>(stateProvinces);
        }

        public async Task DeleteAddress(string accountNumber, string addressType)
        {
            logger.LogInformation("DeleteAddress called");

            logger.LogInformation("Getting customer for {AccountNumber}", accountNumber);
            var customer = await customerApiClient.GetCustomerAsync(accountNumber);
            logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, nameof(customer));

            var customerToUpdate = mapper.Map<ApiClients.CustomerApi.Models.UpdateCustomer.Customer>(customer);
            Guard.Against.Null(customerToUpdate, nameof(customerToUpdate));
            var addressToDelete = customerToUpdate.Addresses.FirstOrDefault(a => a.AddressType == addressType);
            Guard.Against.Null(addressToDelete, nameof(addressToDelete));
            customerToUpdate.Addresses.Remove(addressToDelete);

            logger.LogInformation("Updating customer {@Customer}", customer);
            await customerApiClient.UpdateCustomerAsync(accountNumber, customerToUpdate);
            logger.LogInformation("Customer updated successfully");
        }

        private async Task<IEnumerable<SelectListItem>> GetContactTypes()
        {
            logger.LogInformation("GetContactTypes called.");
            var contactTypes = await referenceDataApiClient.GetContactTypesAsync();
            Guard.Against.Null(contactTypes, nameof(contactTypes));

            var items = contactTypes
                .Select(at => new SelectListItem() { Value = at.Name, Text = at.Name })
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        public async Task<EditCustomerContactViewModel> GetCustomerContact(string accountNumber, string contactName, string contactType)
        {
            logger.LogInformation("GetCustomerContact called");
            var customer = await customerApiClient
                .GetCustomerAsync<ApiClients.CustomerApi.Models.GetCustomer.StoreCustomer>(accountNumber);

            var contact = customer.Contacts.FirstOrDefault(c =>
                c.ContactType == contactType &&
                c.ContactPerson.FullName() == contactName
            );

            var vm = new EditCustomerContactViewModel
            {
                IsNewContact = false,
                AccountNumber = accountNumber,
                CustomerName = customer.Name,
                CustomerContact = mapper.Map<CustomerContactViewModel>(contact),
                ContactTypes = await GetContactTypes()
            };

            return vm;
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
                    ContactPerson = new PersonViewModel
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

            logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await customerApiClient.GetCustomerAsync<ApiClients.CustomerApi.Models.GetCustomer.StoreCustomer>(
                viewModel.AccountNumber
            );
            logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, nameof(customer));

            var customerToUpdate = mapper.Map<ApiClients.CustomerApi.Models.UpdateCustomer.StoreCustomer>(customer);
            Guard.Against.Null(customerToUpdate, nameof(customerToUpdate));
            var contactToAdd = mapper.Map<ApiClients.CustomerApi.Models.UpdateCustomer.StoreCustomerContact>(
                viewModel.CustomerContact
            );
            customerToUpdate.Contacts.Add(contactToAdd);

            logger.LogInformation("Updating customer {@Customer}", customer);
            await customerApiClient.UpdateCustomerAsync(viewModel.AccountNumber, customerToUpdate);
            logger.LogInformation("Customer updated successfully");
        }

        public async Task UpdateContact(EditCustomerContactViewModel viewModel)
        {
            logger.LogInformation("UpdateContact called");

            logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await customerApiClient.GetCustomerAsync<ApiClients.CustomerApi.Models.GetCustomer.StoreCustomer>(
                viewModel.AccountNumber
            );
            logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, nameof(customer));

            var customerToUpdate = mapper.Map<ApiClients.CustomerApi.Models.UpdateCustomer.StoreCustomer>(customer);
            Guard.Against.Null(customerToUpdate, nameof(customerToUpdate));
            var contact = customerToUpdate.Contacts.FirstOrDefault(c => c.ContactType == viewModel.CustomerContact.ContactType);
            Guard.Against.Null(contact, nameof(contact));
            mapper.Map(viewModel.CustomerContact.ContactPerson, contact.ContactPerson);

            logger.LogInformation("Updating customer {@Customer}", customer);
            await customerApiClient.UpdateCustomerAsync(viewModel.AccountNumber, customerToUpdate);
            logger.LogInformation("Customer updated successfully");
        }

        public async Task<DeleteCustomerContactViewModel> GetCustomerContactForDelete(string accountNumber, string contactName, string contactType)
        {
            logger.LogInformation("GetCustomerContactForDelete called");
            var customer = await customerApiClient
                .GetCustomerAsync<ApiClients.CustomerApi.Models.GetCustomer.StoreCustomer>(accountNumber);
            Guard.Against.Null(customer, nameof(customer));

            var contact = customer.Contacts.FirstOrDefault(a =>
                a.ContactType == contactType && a.ContactPerson.FullName() == contactName
            );

            var vm = mapper.Map<DeleteCustomerContactViewModel>(contact);
            vm.AccountNumber = accountNumber;
            vm.CustomerName = customer.Name;

            return vm;
        }

        public async Task DeleteContact(DeleteCustomerContactViewModel viewModel)
        {
            logger.LogInformation("DeleteContact called");

            logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await customerApiClient.GetCustomerAsync<ApiClients.CustomerApi.Models.GetCustomer.StoreCustomer>(
                viewModel.AccountNumber
            );
            logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, nameof(customer));

            var customerToUpdate = mapper.Map<ApiClients.CustomerApi.Models.UpdateCustomer.StoreCustomer>(customer);
            Guard.Against.Null(customerToUpdate, nameof(customerToUpdate));
            var contact = customerToUpdate.Contacts.FirstOrDefault(c => c.ContactType == viewModel.ContactType);
            Guard.Against.Null(contact, nameof(contact));
            customerToUpdate.Contacts.Remove(contact);

            logger.LogInformation("Updating customer {@Customer}", customer);
            await customerApiClient.UpdateCustomerAsync(viewModel.AccountNumber, customerToUpdate);
            logger.LogInformation("Customer updated successfully");
        }

        public EditEmailAddressViewModel AddEmailAddress(string accountNumber, string personName)
        {
            logger.LogInformation("AddEmailAddress called");

            var vm = new EditEmailAddressViewModel
            {
                IsNewEmailAddress = true,
                AccountNumber = accountNumber,
                PersonName = personName
            };

            return vm;
        }

        public async Task AddIndividualCustomerEmailAddress(EditEmailAddressViewModel viewModel)
        {
            logger.LogInformation("AddIndividualCustomerEmailAddress called");

            logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await customerApiClient.GetCustomerAsync<ApiClients.CustomerApi.Models.GetCustomer.IndividualCustomer>(
                viewModel.AccountNumber
            );
            logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, nameof(customer));

            var customerToUpdate = mapper.Map<ApiClients.CustomerApi.Models.UpdateCustomer.IndividualCustomer>(customer);
            Guard.Against.Null(customerToUpdate, nameof(customerToUpdate));

            customerToUpdate.Person.EmailAddresses.Add(new ApiClients.CustomerApi.Models.UpdateCustomer.PersonEmailAddress
            {
                EmailAddress = viewModel.EmailAddress
            });

            logger.LogInformation("Updating customer {@Customer}", customer);
            await customerApiClient.UpdateCustomerAsync(viewModel.AccountNumber, customerToUpdate);
            logger.LogInformation("Customer updated successfully");
        }

        public async Task AddContactEmailAddress(EditEmailAddressViewModel viewModel)
        {
            logger.LogInformation("AddEmailAddress called");

            logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await customerApiClient.GetCustomerAsync<ApiClients.CustomerApi.Models.GetCustomer.StoreCustomer>(
                viewModel.AccountNumber
            );
            logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, nameof(customer));

            var customerToUpdate = mapper.Map<ApiClients.CustomerApi.Models.UpdateCustomer.StoreCustomer>(customer);
            Guard.Against.Null(customerToUpdate, nameof(customerToUpdate));

            var contact = customerToUpdate.Contacts.FirstOrDefault(c => c.ContactPerson.FullName() == viewModel.PersonName);
            Guard.Against.Null(contact, nameof(contact));

            contact.ContactPerson.EmailAddresses.Add(new ApiClients.CustomerApi.Models.UpdateCustomer.PersonEmailAddress
            {
                EmailAddress = viewModel.EmailAddress
            });

            logger.LogInformation("Updating customer {@Customer}", customer);
            await customerApiClient.UpdateCustomerAsync(viewModel.AccountNumber, customerToUpdate);
            logger.LogInformation("Customer updated successfully");
        }

        public async Task<DeleteIndividualCustomerEmailAddressViewModel> GetIndividualCustomerEmailAddressForDelete(string accountNumber, string emailAddress)
        {
            logger.LogInformation("GetIndividualCustomerEmailAddressForDelete called");
            var customer = await customerApiClient
                .GetCustomerAsync<ApiClients.CustomerApi.Models.GetCustomer.IndividualCustomer>(accountNumber);
            Guard.Against.Null(customer, nameof(customer));

            var personEmailAddress = customer.Person.EmailAddresses.FirstOrDefault(c =>
                c.EmailAddress == emailAddress
            );
            Guard.Against.Null(personEmailAddress, nameof(personEmailAddress));

            var vm = mapper.Map<DeleteIndividualCustomerEmailAddressViewModel>(personEmailAddress);
            vm.AccountNumber = accountNumber;
            vm.CustomerName = customer.Person.FullName();

            return vm;
        }

        public async Task<DeleteContactEmailAddressViewModel> GetContactEmailAddressForDelete(string accountNumber, string contactType, string contactName, string emailAddress)
        {
            logger.LogInformation("GetContactEmailAddressForDelete called");
            var customer = await customerApiClient
                .GetCustomerAsync<ApiClients.CustomerApi.Models.GetCustomer.StoreCustomer>(accountNumber);
            Guard.Against.Null(customer, nameof(customer));

            var contact = customer.Contacts.FirstOrDefault(a =>
                a.ContactType == contactType && a.ContactPerson.FullName() == contactName
            );
            Guard.Against.Null(contact, nameof(contact));

            var personEmailAddress = contact.ContactPerson.EmailAddresses.FirstOrDefault(c =>
                c.EmailAddress == emailAddress
            );
            Guard.Against.Null(personEmailAddress, nameof(personEmailAddress));

            var vm = mapper.Map<DeleteContactEmailAddressViewModel>(personEmailAddress);
            vm.AccountNumber = accountNumber;
            vm.ContactName = contactName;

            return vm;
        }

        public DeleteIndividualCustomerEmailAddressViewModel DeleteIndividualCustomerEmailAddress(string accountNumber, string customerName, string emailAddress)
        {
            logger.LogInformation("DeleteEmailAddress called");

            var vm = new DeleteIndividualCustomerEmailAddressViewModel
            {
                AccountNumber = accountNumber,
                CustomerName = customerName,
                EmailAddress = emailAddress
            };

            return vm;
        }

        public async Task DeleteIndividualCustomerEmailAddress(DeleteIndividualCustomerEmailAddressViewModel viewModel)
        {
            logger.LogInformation("DeleteIndividualCustomerEmailAddress called");

            logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await customerApiClient.GetCustomerAsync<ApiClients.CustomerApi.Models.GetCustomer.StoreCustomer>(
                viewModel.AccountNumber
            );
            logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, nameof(customer));

            var customerToUpdate = mapper.Map<ApiClients.CustomerApi.Models.UpdateCustomer.IndividualCustomer>(customer);
            Guard.Against.Null(customerToUpdate, nameof(customerToUpdate));

            var personEmailAddress = customerToUpdate.Person.EmailAddresses.FirstOrDefault(c =>
                c.EmailAddress == viewModel.EmailAddress
            );
            Guard.Against.Null(personEmailAddress, nameof(personEmailAddress));

            customerToUpdate.Person.EmailAddresses.Remove(personEmailAddress);

            logger.LogInformation("Updating customer {@Customer}", customer);
            await customerApiClient.UpdateCustomerAsync(viewModel.AccountNumber, customerToUpdate);
            logger.LogInformation("Customer updated successfully");
        }

        public DeleteContactEmailAddressViewModel DeleteContactEmailAddress(string accountNumber, string contactType, string contactName, string emailAddress)
        {
            logger.LogInformation("DeleteContactEmailAddress called");

            var vm = new DeleteContactEmailAddressViewModel
            {
                AccountNumber = accountNumber,
                ContactType = contactType,
                ContactName = contactName,
                EmailAddress = emailAddress
            };

            return vm;
        }

        public async Task DeleteContactEmailAddress(DeleteContactEmailAddressViewModel viewModel)
        {
            logger.LogInformation("DeleteEmailAddress called");

            logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await customerApiClient.GetCustomerAsync<ApiClients.CustomerApi.Models.GetCustomer.StoreCustomer>(
                viewModel.AccountNumber
            );
            logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, nameof(customer));

            var customerToUpdate = mapper.Map<ApiClients.CustomerApi.Models.UpdateCustomer.StoreCustomer>(customer);
            Guard.Against.Null(customerToUpdate, nameof(customerToUpdate));
            
            var contact = customerToUpdate.Contacts.FirstOrDefault(c => 
                c.ContactType == viewModel.ContactType &&
                c.ContactPerson.FullName() == viewModel.ContactName);
            Guard.Against.Null(contact, nameof(contact));

            var personEmailAddress = contact.ContactPerson.EmailAddresses.FirstOrDefault(c =>
                c.EmailAddress == viewModel.EmailAddress
            );
            Guard.Against.Null(personEmailAddress, nameof(personEmailAddress));

            contact.ContactPerson.EmailAddresses.Remove(personEmailAddress);

            logger.LogInformation("Updating customer {@Customer}", customer);
            await customerApiClient.UpdateCustomerAsync(viewModel.AccountNumber, customerToUpdate);
            logger.LogInformation("Customer updated successfully");
        }
    }
}