using Ardalis.GuardClauses;
using AutoMapper;
using AW.UI.Web.Admin.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.Extensions;
using MediatR;
using AW.UI.Web.SharedKernel.SalesPerson.Handlers.GetSalesPersons;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetStatesProvinces;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetContactTypes;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomers;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetCustomer;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetStoreCustomer;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetIndividualCustomer;
using AW.UI.Web.SharedKernel.Customer.Handlers.UpdateCustomer;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetAddressTypes;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetCountries;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CustomerService(
            ILogger<CustomerService> logger,
            IMapper mapper,
            IMediator mediator
        )
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<CustomersIndexViewModel> GetCustomers(int pageIndex, int pageSize, string? territory, CustomerType? customerType, string? accountNumber)
        {
            _logger.LogInformation("GetCustomers called");
            var response = await _mediator.Send(new GetCustomersQuery(
                    pageIndex,
                    pageSize,
                    territory,
                    customerType,
                    accountNumber
                )
            );

            var totalPages = int.Parse(Math.Ceiling((decimal)response.TotalCustomers / pageSize).ToString());

            var vm = new CustomersIndexViewModel
            {
                Customers = _mapper.Map<List<CustomerViewModel>>(response.Customers),
                Territories = await GetTerritories(),
                CustomerTypes = GetCustomerTypes(),
                PaginationInfo = new PaginationInfoViewModel(
                    response.TotalCustomers,
                    response.Customers!.Count,
                    pageIndex,
                    totalPages,
                    pageIndex == 0 ? "disabled" : "",
                    pageIndex == totalPages - 1 ? "disabled" : ""
                )
            };

            return vm;
        }

        public async Task<CustomerViewModel> GetCustomer(string? accountNumber)
        {
            _logger.LogInformation("GetCustomer called");
            var customer = await _mediator.Send(new GetCustomerQuery(accountNumber));

            return _mapper.Map<CustomerViewModel>(customer);
        }

        public async Task<IEnumerable<SelectListItem>?> GetTerritories()
        {
            _logger.LogInformation("GetTerritories called.");
            var territories = await _mediator.Send(new GetTerritoriesQuery());

            var items = territories
                .Select(t => new SelectListItem() { Value = t.Name, Text = $"{t.Name} ({t.CountryRegionCode})" })
                .OrderBy(b => b.Text)
                .ToList();

            items.Insert(0, new SelectListItem { Value = "", Text = "--Select--", Selected = true });

            return items;
        }       

        private List<SelectListItem> GetCustomerTypes()
        {
            _logger.LogInformation("GetCustomerTypes called.");

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = "", Text = "All", Selected = true },
                new SelectListItem() { Value = "Individual", Text = "Individual"},
                new SelectListItem() { Value = "Store", Text = "Store" }
            };

            return items;
        }

        public async Task<IEnumerable<SelectListItem>?> GetSalesPersons(string territory)
        {
            _logger.LogInformation("GetSalesPersons called.");
            var salesPersons = await _mediator.Send(new GetSalesPersonsQuery(territory));

            var items = salesPersons
                .Select(t => new SelectListItem() { Value = t.Name?.FullName, Text = t.Name?.FullName })
                .OrderBy(b => b.Text)
                .ToList();

            return items;
        }

        public async Task<IEnumerable<SelectListItem>?> GetAddressTypes()
        {
            _logger.LogInformation("GetAddressTypes called.");
            var addressTypes = await _mediator.Send(new GetAddressTypesQuery());

            var items = addressTypes
                .Select(t => new SelectListItem() { Value = t.Name, Text = t.Name })
                .OrderBy(b => b.Text)
                .ToList();

            items.Insert(0, new SelectListItem { Value = "", Text = "--Select", Selected = true });

            return items;
        }

        public async Task<IEnumerable<SelectListItem>?> GetCountries()
        {
            _logger.LogInformation("GetCountries called.");
            var countries = await _mediator.Send(new GetCountriesQuery());

            var items = countries
                .Select(t => new SelectListItem() { Value = t.CountryRegionCode, Text = t.Name })
                .OrderBy(b => b.Text)
                .ToList();

            items.Insert(0, new SelectListItem { Value = "", Text = "--Select", Selected = true });

            return items;
        }

        public async Task UpdateStore(StoreCustomerViewModel? viewModel)
        {
            _logger.LogInformation("UpdateStore called with view model {@ViewModel}", viewModel);
            Guard.Against.Null(viewModel, _logger);

            _logger.LogInformation("Mapping CustomerViewModel to UpdateCustomerRequest");
            var storeCustomer = await _mediator.Send(new GetStoreCustomerQuery(viewModel?.AccountNumber));
            var storeCustomerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer>(storeCustomer);

            storeCustomerToUpdate.Name = viewModel?.Name;
            storeCustomerToUpdate.Territory = viewModel?.Territory;
            storeCustomerToUpdate.SalesPerson = viewModel?.SalesPerson;

            _logger.LogInformation("Calling Customer API to update customer");
            await _mediator.Send(new UpdateCustomerCommand(viewModel?.AccountNumber, storeCustomerToUpdate));
            _logger.LogInformation("Customer successfully updated");
        }

        public async Task UpdateIndividual(IndividualCustomerViewModel? viewModel)
        {
            _logger.LogInformation("UpdateIndividual called with view model {@ViewModel}", viewModel);
            Guard.Against.Null(viewModel, _logger);

            var customer = await _mediator.Send(new GetIndividualCustomerQuery(viewModel?.AccountNumber));
            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>(customer);

            customerToUpdate.Person = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.Person>(viewModel?.Person);

            _logger.LogInformation("Calling Customer API to update customer");
            await _mediator.Send(new UpdateCustomerCommand(viewModel?.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer successfully updated");
        }

        //public EditCustomerAddressViewModel AddAddress(string accountNumber, string customerName)
        //{
        //    _logger.LogInformation("AddAddress called");

        //    var vm = new EditCustomerAddressViewModel
        //    {
        //        IsNewAddress = true,
        //        AccountNumber = accountNumber,
        //        CustomerName = customerName,
        //        CustomerAddress = new CustomerAddressViewModel
        //        {
        //            Address = new AddressViewModel
        //            {
        //                CountryRegionCode = "US"
        //            }
        //        }
        //    };

        //    return vm;
        //}

        public async Task AddAddress(CustomerAddressViewModel viewModel, string? accountNumber)
        {
            _logger.LogInformation("AddAddress called");

            _logger.LogInformation("Getting customer for {AccountNumber}", accountNumber);
            var customer = await _mediator.Send(new GetCustomerQuery(accountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.Customer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);
            var newAddress = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.CustomerAddress>(viewModel);
            Guard.Against.Null(newAddress, _logger);
            customerToUpdate.Addresses?.Add(newAddress);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(accountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public async Task UpdateAddress(CustomerAddressViewModel viewModel, string? accountNumber)
        {
            _logger.LogInformation("UpdateAddress called");

            _logger.LogInformation("Getting customer for {AccountNumber}", accountNumber);
            var customer = await _mediator.Send(new GetCustomerQuery(accountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.Customer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);
            var addressToUpdate = customerToUpdate.Addresses?.FirstOrDefault(a => a?.AddressType == viewModel.AddressType);
            Guard.Against.Null(addressToUpdate, _logger);
            _mapper.Map(viewModel.Address, addressToUpdate?.Address);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(accountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public async Task<IEnumerable<SelectListItem>?> GetStatesProvinces(string countryRegionCode)
        {
            _logger.LogInformation("GetStateProvinces called.");
            var statesProvinces = await _mediator.Send(new GetStatesProvincesQuery(countryRegionCode));

            var items = statesProvinces
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem() { Value = c.StateProvinceCode, Text = c.Name })
                .ToList();

            items.Insert(0, new SelectListItem { Value = "", Text = "--Select--", Selected = true });

            return items;
        }

        public async Task<IEnumerable<StateProvince>?> GetStatesProvincesJson(string? country)
        {
            var statesProvinces = await _mediator.Send(new GetStatesProvincesQuery(country));
            return statesProvinces;
        }

        public async Task DeleteAddress(string? accountNumber, string? addressType)
        {
            _logger.LogInformation("DeleteAddress called");

            _logger.LogInformation("Getting customer for {AccountNumber}", accountNumber);
            var customer = await _mediator.Send(new GetCustomerQuery(accountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.Customer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);
            var addressToDelete = customerToUpdate.Addresses?.FirstOrDefault(a => a?.AddressType == addressType);
            Guard.Against.Null(addressToDelete, _logger);
            customerToUpdate.Addresses?.Remove(addressToDelete);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(accountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        private async Task<IEnumerable<SelectListItem>> GetContactTypes()
        {
            _logger.LogInformation("GetContactTypes called.");
            var contactTypes = await _mediator.Send(new GetContactTypesQuery());
            Guard.Against.Null(contactTypes, _logger);

            var items = contactTypes
                .Select(at => new SelectListItem() { Value = at.Name, Text = at.Name })
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        public IEnumerable<SelectListItem> GetPhoneNumberTypes()
        {
            var phoneNumberTypes = new[] { "Cell", "Home", "Work" };
            var items = phoneNumberTypes.Select(
                    _ => new SelectListItem(_, _)
                )
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        public async Task<StoreCustomerContactViewModel> GetCustomerContact(string? accountNumber, string? contactName)
        {
            _logger.LogInformation("GetCustomerContact called");
            var customer = await _mediator.Send(new GetStoreCustomerQuery(accountNumber));

            var contact = customer.Contacts.FirstOrDefault(c =>
                c.ContactPerson?.Name?.FullName == contactName
            );

            var vm = new StoreCustomerContactViewModel
            {
                IsNewContact = false,
                AccountNumber = accountNumber,
                CustomerName = customer.Name,
                CustomerContact = _mapper.Map<CustomerContactViewModel>(contact),
                ContactTypes = await GetContactTypes(),
                PhoneNumberTypes = GetPhoneNumberTypes()
            };

            return vm;
        }

        public async Task<StoreCustomerContactViewModel> AddContact(string? accountNumber, string? customerName)
        {
            _logger.LogInformation("AddContact called");

            var vm = new StoreCustomerContactViewModel
            {
                IsNewContact = true,
                AccountNumber = accountNumber,
                CustomerName = customerName,
                CustomerContact = new CustomerContactViewModel
                {
                    ContactPerson = new PersonViewModel
                    {
                        EmailAddresses = new(),
                        PhoneNumbers = new()
                    }
                },
                ContactTypes = await GetContactTypes(),
                PhoneNumberTypes = GetPhoneNumberTypes()
            };

            return vm;
        }

        public async Task AddContact(StoreCustomerContactViewModel viewModel)
        {
            _logger.LogInformation("AddContact called");

            _logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await _mediator.Send(new GetStoreCustomerQuery(viewModel.AccountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);
            var contactToAdd = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomerContact>(
                viewModel.CustomerContact
            );
            customerToUpdate.Contacts?.Add(contactToAdd);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public async Task UpdateContact(StoreCustomerContactViewModel? viewModel)
        {
            _logger.LogInformation("UpdateContact called");

            _logger.LogInformation("Getting customer for {AccountNumber}", viewModel?.AccountNumber);
            var customer = await _mediator.Send(new GetStoreCustomerQuery(viewModel?.AccountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);
            var contact = customerToUpdate.Contacts?.FirstOrDefault(c => c?.ContactPerson?.Name?.FullName == viewModel?.CustomerContact?.ContactPerson.Name!.FullName);
            Guard.Against.Null(contact, _logger);
            _mapper.Map(viewModel?.CustomerContact, contact);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(viewModel?.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public async Task DeleteContact(string? accountNumber, string? contactName)
        {
            _logger.LogInformation("DeleteContact called");

            _logger.LogInformation("Getting customer for {AccountNumber}", accountNumber);
            var customer = await _mediator.Send(new GetStoreCustomerQuery(accountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);
            var contact = customerToUpdate.Contacts?.FirstOrDefault(c => c?.ContactPerson?.Name?.FullName == contactName);
            Guard.Against.Null(contact, _logger);
            customerToUpdate.Contacts?.Remove(contact);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(accountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public async Task DeleteContactEmailAddress(string? accountNumber, string? contactName, string? emailAddress)
        {
            _logger.LogInformation("DeleteContactEmailAddress called");

            _logger.LogInformation("Getting customer for {AccountNumber}", accountNumber);
            var customer = await _mediator.Send(new GetStoreCustomerQuery(accountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);

            var contact = customerToUpdate?.Contacts?.FirstOrDefault(c =>
                c?.ContactPerson?.Name?.FullName == contactName
            );
            Guard.Against.Null(contact, _logger);

            var personEmailAddress = contact?.ContactPerson?.EmailAddresses?.FirstOrDefault(c =>
                c?.EmailAddress == emailAddress
            );
            Guard.Against.Null(personEmailAddress, _logger);

            contact?.ContactPerson?.EmailAddresses?.Remove(personEmailAddress);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(accountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public async Task DeleteIndividualCustomerEmailAddress(string? accountNumber, string? emailAddress)
        {
            _logger.LogInformation("DeleteContactEmailAddress called");

            _logger.LogInformation("Getting customer for {AccountNumber}", accountNumber);
            var customer = await _mediator.Send(new GetIndividualCustomerQuery(accountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);

            var personEmailAddress = customerToUpdate.Person?.EmailAddresses?.FirstOrDefault(c =>
                c?.EmailAddress == emailAddress
            );
            Guard.Against.Null(personEmailAddress, _logger);

            customerToUpdate.Person?.EmailAddresses?.Remove(personEmailAddress);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(accountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }
    }
}