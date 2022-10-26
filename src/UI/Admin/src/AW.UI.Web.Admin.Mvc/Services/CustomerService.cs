using Ardalis.GuardClauses;
using AutoMapper;
using AW.UI.Web.Admin.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<CustomersIndexViewModel> GetCustomers(int pageIndex, int pageSize, string territory, CustomerType? customerType, string accountNumber)
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

            var vm = new CustomersIndexViewModel
            {
                Customers = _mapper.Map<List<CustomerViewModel>>(response.Customers),
                Territories = await GetTerritories(false),
                CustomerTypes = GetCustomerTypes(),
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = response.Customers.Count,
                    TotalItems = response.TotalCustomers,
                    TotalPages = int.Parse(Math.Ceiling((decimal)response.TotalCustomers / pageSize).ToString())
                }
            };

            vm.PaginationInfo.Next = vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1 ? "disabled" : "";
            vm.PaginationInfo.Previous = vm.PaginationInfo.ActualPage == 0 ? "disabled" : "";

            return vm;
        }

        public async Task<CustomerDetailViewModel> GetCustomer(string accountNumber)
        {
            _logger.LogInformation("GetCustomer called");
            var customer = await _mediator.Send(new GetCustomerQuery(accountNumber));

            var vm = new CustomerDetailViewModel
            {
                Customer = _mapper.Map<CustomerViewModel>(customer)
            };

            return vm;
        }

        public async Task<EditStoreCustomerViewModel> GetStoreCustomerForEdit(string accountNumber)
        {
            _logger.LogInformation("GetStoreCustomerForEdit called");

            _logger.LogInformation("Getting customer for {AccountNumber}", accountNumber);
            var customer = await _mediator.Send(new GetStoreCustomerQuery(accountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);

            var vm = new EditStoreCustomerViewModel
            {
                Customer = _mapper.Map<StoreCustomerViewModel>(customer),
                Territories = await GetTerritories(true),
                SalesPersons = await GetSalesPersons(customer.Territory)
            };

            return vm;
        }

        public async Task<EditIndividualCustomerViewModel> GetIndividualCustomerForEdit(string accountNumber)
        {
            _logger.LogInformation("GetIndividualCustomerForEdit called");

            _logger.LogInformation("Getting customer for {AccountNumber}", accountNumber);
            var customer = await _mediator.Send(new GetIndividualCustomerQuery(accountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);

            var vm = new EditIndividualCustomerViewModel
            {
                Customer = _mapper.Map<IndividualCustomerViewModel>(customer),
                Territories = await GetTerritories(true),
                EmailPromotions = GetEmailPromotions()
            };

            return vm;
        }

        private async Task<IEnumerable<SelectListItem>> GetTerritories(bool edit)
        {
            _logger.LogInformation("GetTerritories called.");
            var territories = await _mediator.Send(new GetTerritoriesQuery());

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

        private static IEnumerable<SelectListItem> GetEmailPromotions()
        {
            var items = new List<SelectListItem>();

            foreach (EmailPromotionViewModel emailPromotion in (EmailPromotionViewModel[])Enum.GetValues(typeof(EmailPromotionViewModel)))
            {
                items.Add(new SelectListItem(
                        Enum<EmailPromotionViewModel>.GetDisplayValue(emailPromotion),
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
            _logger.LogInformation("GetCustomerTypes called.");

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = "", Text = "All", Selected = true },
                new SelectListItem() { Value = "Individual", Text = "Individual"},
                new SelectListItem() { Value = "Store", Text = "Store" }
            };

            return items;
        }

        private async Task<IEnumerable<SelectListItem>> GetSalesPersons(string territory)
        {
            _logger.LogInformation("GetSalesPersons called.");
            var salesPersons = await _mediator.Send(new GetSalesPersonsQuery(territory));

            var items = salesPersons
                .Select(t => new SelectListItem() { Value = t.Name.FullName, Text = t.Name.FullName })
                .OrderBy(b => b.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        public async Task UpdateStore(StoreCustomerViewModel viewModel)
        {
            _logger.LogInformation("UpdateStore called with view model {@ViewModel}", viewModel);
            Guard.Against.Null(viewModel, _logger);

            _logger.LogInformation("Mapping CustomerViewModel to UpdateCustomerRequest");
            var storeCustomer = await _mediator.Send(new GetStoreCustomerQuery(viewModel.AccountNumber));
            var storeCustomerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer>(storeCustomer);

            storeCustomerToUpdate.Name = viewModel.Name;
            storeCustomerToUpdate.Territory = viewModel.Territory;
            storeCustomerToUpdate.SalesPerson = viewModel.SalesPerson;

            _logger.LogInformation("Calling Customer API to update customer");
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, storeCustomerToUpdate));
            _logger.LogInformation("Customer successfully updated");
        }

        public async Task UpdateIndividual(IndividualCustomerViewModel viewModel)
        {
            _logger.LogInformation("UpdateIndividual called with view model {@ViewModel}", viewModel);
            Guard.Against.Null(viewModel, _logger);

            var individualCustomer = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>(viewModel);

            _logger.LogInformation("Calling Customer API to update customer");
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, individualCustomer));
            _logger.LogInformation("Customer successfully updated");
        }

        public EditCustomerAddressViewModel AddAddress(string accountNumber, string customerName)
        {
            _logger.LogInformation("AddAddress called");

            var vm = new EditCustomerAddressViewModel
            {
                IsNewAddress = true,
                AccountNumber = accountNumber,
                CustomerName = customerName,
                CustomerAddress = new CustomerAddressViewModel
                {
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
            _logger.LogInformation("AddAddress called");

            _logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await _mediator.Send(new GetCustomerQuery(viewModel.AccountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.Customer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);
            var newAddress = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.CustomerAddress>(viewModel.CustomerAddress);
            Guard.Against.Null(newAddress, _logger);
            customerToUpdate.Addresses.Add(newAddress);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public async Task<EditCustomerAddressViewModel> GetCustomerAddress(string accountNumber, string addressType)
        {
            _logger.LogInformation("GetCustomerAddress called");
            var customer = await _mediator.Send(new GetCustomerQuery(accountNumber));
            Guard.Against.Null(customer, _logger);

            var address = customer.Addresses.FirstOrDefault(a => a.AddressType == addressType);
            Guard.Against.Null(address, _logger);

            var vm = new EditCustomerAddressViewModel
            {
                AccountNumber = accountNumber,
                CustomerName = customer.CustomerName,
                CustomerAddress = _mapper.Map<CustomerAddressViewModel>(address),
            };

            return vm;
        }

        public async Task UpdateAddress(EditCustomerAddressViewModel viewModel)
        {
            _logger.LogInformation("UpdateAddress called");

            _logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await _mediator.Send(new GetCustomerQuery(viewModel.AccountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.Customer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);
            var addressToUpdate = customerToUpdate.Addresses.FirstOrDefault(a => a.AddressType == viewModel.CustomerAddress.AddressType);
            Guard.Against.Null(addressToUpdate, _logger);
            _mapper.Map(viewModel.CustomerAddress.Address, addressToUpdate.Address);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public async Task<IEnumerable<SelectListItem>> GetStatesProvinces(string country)
        {
            _logger.LogInformation("GetStateProvinces called.");
            var statesProvinces = await _mediator.Send(new GetStatesProvincesQuery(country));

            var items = statesProvinces
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem() { Value = c.StateProvinceCode, Text = c.Name })
                .ToList();

            var allItem = new SelectListItem() { Value = "", Text = "--Select--", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        public async Task<DeleteCustomerAddressViewModel> GetCustomerAddressForDelete(string accountNumber, string addressType)
        {
            _logger.LogInformation("GetCustomerAddressForDelete called");
            var customer = await _mediator.Send(new GetCustomerQuery(accountNumber));
            var address = customer.Addresses.FirstOrDefault(a => a.AddressType == addressType);

            var vm = _mapper.Map<DeleteCustomerAddressViewModel>(address);
            vm.AccountNumber = accountNumber;
            vm.CustomerName = customer.CustomerName;

            return vm;
        }

        public async Task<IEnumerable<StateProvince>> GetStatesProvincesJson(string country)
        {
            var statesProvinces = await _mediator.Send(new GetStatesProvincesQuery(country));
            return statesProvinces;
        }

        public async Task DeleteAddress(string accountNumber, string addressType)
        {
            _logger.LogInformation("DeleteAddress called");

            _logger.LogInformation("Getting customer for {AccountNumber}", accountNumber);
            var customer = await _mediator.Send(new GetCustomerQuery(accountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.Customer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);
            var addressToDelete = customerToUpdate.Addresses.FirstOrDefault(a => a.AddressType == addressType);
            Guard.Against.Null(addressToDelete, _logger);
            customerToUpdate.Addresses.Remove(addressToDelete);

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

        public async Task<EditCustomerContactViewModel> GetCustomerContact(string accountNumber, string contactName, string contactType)
        {
            _logger.LogInformation("GetCustomerContact called");
            var customer = await _mediator.Send(new GetStoreCustomerQuery(accountNumber));

            var contact = customer.Contacts.FirstOrDefault(c =>
                c.ContactType == contactType &&
                c.ContactPerson.Name.FullName == contactName
            );

            var vm = new EditCustomerContactViewModel
            {
                IsNewContact = false,
                AccountNumber = accountNumber,
                CustomerName = customer.Name,
                CustomerContact = _mapper.Map<CustomerContactViewModel>(contact),
                ContactTypes = await GetContactTypes()
            };

            return vm;
        }

        public async Task<EditCustomerContactViewModel> AddContact(string accountNumber, string customerName)
        {
            _logger.LogInformation("AddContact called");

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
            customerToUpdate.Contacts.Add(contactToAdd);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public async Task UpdateContact(EditCustomerContactViewModel viewModel)
        {
            _logger.LogInformation("UpdateContact called");

            _logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await _mediator.Send(new GetStoreCustomerQuery(viewModel.AccountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);
            var contact = customerToUpdate.Contacts.FirstOrDefault(c => c.ContactType == viewModel.CustomerContact.ContactType);
            Guard.Against.Null(contact, _logger);
            _mapper.Map(viewModel.CustomerContact.ContactPerson, contact.ContactPerson);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public async Task<DeleteCustomerContactViewModel> GetCustomerContactForDelete(string accountNumber, string contactName, string contactType)
        {
            _logger.LogInformation("GetCustomerContactForDelete called");
            var customer = await _mediator.Send(new GetStoreCustomerQuery(accountNumber));
            Guard.Against.Null(customer, _logger);

            var contact = customer.Contacts.FirstOrDefault(a =>
                a.ContactType == contactType && a.ContactPerson.Name.FullName == contactName
            );

            var vm = _mapper.Map<DeleteCustomerContactViewModel>(contact);
            vm.AccountNumber = accountNumber;
            vm.CustomerName = customer.Name;

            return vm;
        }

        public async Task DeleteContact(DeleteCustomerContactViewModel viewModel)
        {
            _logger.LogInformation("DeleteContact called");

            _logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await _mediator.Send(new GetStoreCustomerQuery(viewModel.AccountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);
            var contact = customerToUpdate.Contacts.FirstOrDefault(c => c.ContactType == viewModel.ContactType);
            Guard.Against.Null(contact, _logger);
            customerToUpdate.Contacts.Remove(contact);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public EditEmailAddressViewModel AddEmailAddress(string accountNumber, string customerName)
        {
            _logger.LogInformation("AddEmailAddress called");

            var vm = new EditEmailAddressViewModel
            {
                IsNewEmailAddress = true,
                AccountNumber = accountNumber,
                PersonName = customerName
            };

            return vm;
        }

        public async Task AddIndividualCustomerEmailAddress(EditEmailAddressViewModel viewModel)
        {
            _logger.LogInformation("AddIndividualCustomerEmailAddress called");

            _logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await _mediator.Send(new GetIndividualCustomerQuery(viewModel.AccountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);

            customerToUpdate.Person.EmailAddresses.Add(new SharedKernel.Customer.Handlers.UpdateCustomer.PersonEmailAddress
            {
                EmailAddress = viewModel.EmailAddress
            });

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public async Task AddContactEmailAddress(EditEmailAddressViewModel viewModel)
        {
            _logger.LogInformation("AddEmailAddress called");

            _logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await _mediator.Send(new GetStoreCustomerQuery(viewModel.AccountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);

            var contact = customerToUpdate.Contacts.FirstOrDefault(c => c.ContactPerson.Name.FullName == viewModel.PersonName);
            Guard.Against.Null(contact, _logger);

            contact.ContactPerson.EmailAddresses.Add(new SharedKernel.Customer.Handlers.UpdateCustomer.PersonEmailAddress
            {
                EmailAddress = viewModel.EmailAddress
            });

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public async Task<DeleteIndividualCustomerEmailAddressViewModel> GetIndividualCustomerEmailAddressForDelete(string accountNumber, string emailAddress)
        {
            _logger.LogInformation("GetIndividualCustomerEmailAddressForDelete called");
            var customer = await _mediator.Send(new GetIndividualCustomerQuery(accountNumber));
            Guard.Against.Null(customer, _logger);

            var personEmailAddress = customer.Person.EmailAddresses.FirstOrDefault(c =>
                c.EmailAddress == emailAddress
            );
            Guard.Against.Null(personEmailAddress, _logger);

            var vm = _mapper.Map<DeleteIndividualCustomerEmailAddressViewModel>(personEmailAddress);
            vm.AccountNumber = accountNumber;
            vm.CustomerName = customer.Person.Name.FullName;

            return vm;
        }

        public async Task<DeleteContactEmailAddressViewModel> GetContactEmailAddressForDelete(string accountNumber, string contactType, string contactName, string emailAddress)
        {
            _logger.LogInformation("GetContactEmailAddressForDelete called");
            var customer = await _mediator.Send(new GetStoreCustomerQuery(accountNumber));
            Guard.Against.Null(customer, _logger);

            var contact = customer.Contacts.FirstOrDefault(a =>
                a.ContactType == contactType && a.ContactPerson.Name.FullName == contactName
            );
            Guard.Against.Null(contact, _logger);

            var personEmailAddress = contact.ContactPerson.EmailAddresses.FirstOrDefault(c =>
                c.EmailAddress == emailAddress
            );
            Guard.Against.Null(personEmailAddress, _logger);

            var vm = _mapper.Map<DeleteContactEmailAddressViewModel>(personEmailAddress);
            vm.AccountNumber = accountNumber;
            vm.ContactName = contactName;

            return vm;
        }

        public DeleteIndividualCustomerEmailAddressViewModel DeleteIndividualCustomerEmailAddress(string accountNumber, string customerName, string emailAddress)
        {
            _logger.LogInformation("DeleteEmailAddress called");

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
            _logger.LogInformation("DeleteIndividualCustomerEmailAddress called");

            _logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await _mediator.Send(new GetIndividualCustomerQuery(viewModel.AccountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);

            var personEmailAddress = customerToUpdate.Person.EmailAddresses.FirstOrDefault(c =>
                c.EmailAddress == viewModel.EmailAddress
            );
            Guard.Against.Null(personEmailAddress, _logger);

            customerToUpdate.Person.EmailAddresses.Remove(personEmailAddress);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public DeleteContactEmailAddressViewModel DeleteContactEmailAddress(string accountNumber, string contactType, string contactName, string emailAddress)
        {
            _logger.LogInformation("DeleteContactEmailAddress called");

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
            _logger.LogInformation("DeleteEmailAddress called");

            _logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await _mediator.Send(new GetStoreCustomerQuery(viewModel.AccountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);

            var contact = customerToUpdate.Contacts.FirstOrDefault(c =>
                c.ContactType == viewModel.ContactType &&
                c.ContactPerson.Name.FullName == viewModel.ContactName);
            Guard.Against.Null(contact, _logger);

            var personEmailAddress = contact.ContactPerson.EmailAddresses.FirstOrDefault(c =>
                c.EmailAddress == viewModel.EmailAddress
            );
            Guard.Against.Null(personEmailAddress, _logger);

            contact.ContactPerson.EmailAddresses.Remove(personEmailAddress);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public EditPhoneNumberViewModel AddPhoneNumber(string accountNumber, string personName)
        {
            _logger.LogInformation("AddPhoneNumber called");

            var vm = new EditPhoneNumberViewModel
            {
                IsNewPhoneNumber = true,
                AccountNumber = accountNumber,
                PersonName = personName
            };

            return vm;
        }

        public async Task AddIndividualCustomerPhoneNumber(EditPhoneNumberViewModel viewModel)
        {
            _logger.LogInformation("AddIndividualCustomerPhoneNumber called");

            _logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await _mediator.Send(new GetIndividualCustomerQuery(viewModel.AccountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);

            customerToUpdate.Person.PhoneNumbers.Add(new SharedKernel.Customer.Handlers.UpdateCustomer.PersonPhone
            {
                PhoneNumberType = viewModel.PhoneNumberType,
                PhoneNumber = viewModel.PhoneNumber
            });

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public async Task AddContactPhoneNumber(EditPhoneNumberViewModel viewModel)
        {
            _logger.LogInformation("AddContactPhoneNumber called");

            _logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await _mediator.Send(new GetStoreCustomerQuery(viewModel.AccountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);

            var contact = customerToUpdate.Contacts.FirstOrDefault(c => c.ContactPerson.Name.FullName == viewModel.PersonName);
            Guard.Against.Null(contact, _logger);

            contact.ContactPerson.PhoneNumbers.Add(new SharedKernel.Customer.Handlers.UpdateCustomer.PersonPhone
            {
                PhoneNumberType = viewModel.PhoneNumberType,
                PhoneNumber = viewModel.PhoneNumber
            });

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public async Task<DeleteIndividualCustomerPhoneNumberViewModel> GetIndividualCustomerPhoneNumberForDelete(string accountNumber, string phoneNumberType, string phoneNumber)
        {
            _logger.LogInformation("GetIndividualCustomerPhoneNumberForDelete called");
            var customer = await _mediator.Send(new GetIndividualCustomerQuery(accountNumber));
            Guard.Against.Null(customer, _logger);

            var personPhone = customer.Person.PhoneNumbers.FirstOrDefault(p =>
                p.PhoneNumberType == phoneNumberType && p.PhoneNumber == phoneNumber
            );
            Guard.Against.Null(personPhone, _logger);

            var vm = _mapper.Map<DeleteIndividualCustomerPhoneNumberViewModel>(personPhone);
            vm.AccountNumber = accountNumber;
            vm.CustomerName = customer.Person.Name.FullName;

            return vm;
        }

        public async Task<DeleteContactPhoneNumberViewModel> GetContactPhoneNumberForDelete(string accountNumber, string contactType, string contactName, string phoneNumberType, string phoneNumber)
        {
            _logger.LogInformation("GetContactPhoneNumberForDelete called");
            var customer = await _mediator.Send(new GetStoreCustomerQuery(accountNumber));
            Guard.Against.Null(customer, _logger);

            var contact = customer.Contacts.FirstOrDefault(a =>
                a.ContactType == contactType && a.ContactPerson.Name.FullName == contactName
            );
            Guard.Against.Null(contact, _logger);

            var personPhoneNumber = contact.ContactPerson.PhoneNumbers.FirstOrDefault(p =>
                p.PhoneNumberType == phoneNumberType && p.PhoneNumber == phoneNumber
            );
            Guard.Against.Null(personPhoneNumber, _logger);

            var vm = _mapper.Map<DeleteContactPhoneNumberViewModel>(personPhoneNumber);
            vm.AccountNumber = accountNumber;
            vm.ContactName = contactName;

            return vm;
        }

        public DeleteIndividualCustomerPhoneNumberViewModel DeleteIndividualCustomerPhoneNumber(string accountNumber, string customerName, string phoneNumberType, string phoneNumber)
        {
            _logger.LogInformation("DeleteIndividualCustomerPhoneNumber called");

            var vm = new DeleteIndividualCustomerPhoneNumberViewModel
            {
                AccountNumber = accountNumber,
                CustomerName = customerName,
                PhoneNumberType = phoneNumberType,
                PhoneNumber = phoneNumber
            };

            return vm;
        }

        public async Task DeleteIndividualCustomerPhoneNumber(DeleteIndividualCustomerPhoneNumberViewModel viewModel)
        {
            _logger.LogInformation("DeleteIndividualCustomerPhoneNumber called");

            _logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await _mediator.Send(new GetIndividualCustomerQuery(viewModel.AccountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.IndividualCustomer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);

            var personPhone = customerToUpdate.Person.PhoneNumbers.FirstOrDefault(p =>
                p.PhoneNumberType == viewModel.PhoneNumberType && p.PhoneNumber == viewModel.PhoneNumber
            );
            Guard.Against.Null(personPhone, _logger);

            customerToUpdate.Person.PhoneNumbers.Remove(personPhone);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }

        public DeleteContactPhoneNumberViewModel DeleteContactPhoneNumber(string accountNumber, string contactType, string contactName, string phoneNumberType, string phoneNumber)
        {
            _logger.LogInformation("DeleteContactPhoneNumber called");

            var vm = new DeleteContactPhoneNumberViewModel
            {
                AccountNumber = accountNumber,
                ContactType = contactType,
                ContactName = contactName,
                PhoneNumberType = phoneNumberType,
                PhoneNumber = phoneNumber
            };

            return vm;
        }

        public async Task DeleteContactPhoneNumber(DeleteContactPhoneNumberViewModel viewModel)
        {
            _logger.LogInformation("DeleteContactPhoneNumber called");

            _logger.LogInformation("Getting customer for {AccountNumber}", viewModel.AccountNumber);
            var customer = await _mediator.Send(new GetStoreCustomerQuery(viewModel.AccountNumber));
            _logger.LogInformation("Retrieved customer {@Customer}", customer);
            Guard.Against.Null(customer, _logger);

            var customerToUpdate = _mapper.Map<SharedKernel.Customer.Handlers.UpdateCustomer.StoreCustomer>(customer);
            Guard.Against.Null(customerToUpdate, _logger);

            var contact = customerToUpdate.Contacts.FirstOrDefault(c =>
                c.ContactType == viewModel.ContactType &&
                c.ContactPerson.Name.FullName == viewModel.ContactName);
            Guard.Against.Null(contact, _logger);

            var personPhone = contact.ContactPerson.PhoneNumbers.FirstOrDefault(p =>
                p.PhoneNumberType == viewModel.PhoneNumberType && p.PhoneNumber == viewModel.PhoneNumber
            );
            Guard.Against.Null(personPhone, _logger);

            contact.ContactPerson.PhoneNumbers.Remove(personPhone);

            _logger.LogInformation("Updating customer {@Customer}", customer);
            await _mediator.Send(new UpdateCustomerCommand(viewModel.AccountNumber, customerToUpdate));
            _logger.LogInformation("Customer updated successfully");
        }
    }
}