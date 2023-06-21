using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using AW.SharedKernel.Extensions;
using MediatR;
using AW.UI.Web.Infrastructure.Api.SalesPerson.Handlers.GetSalesPersons;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetTerritories;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetStatesProvinces;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetContactTypes;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomer;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetStoreCustomer;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetIndividualCustomer;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetAddressTypes;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetCountries;
using UpdateCustomer = AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomers;
using AW.SharedKernel.ValueTypes;

namespace AW.UI.Web.Admin.Mvc.Services;

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

    public async Task<List<CustomerViewModel>> GetCustomers()
    {
        _logger.LogInformation("Getting customers from cache");

        var customers = await _mediator.Send(new GetCustomersQuery());

        _logger.LogInformation("Returning customers");
        return _mapper.Map<List<CustomerViewModel>>(customers);
    }

    public async Task<StoreCustomerViewModel> GetDetailStore(Guid objectId)
    {
        var customer = await GetCustomer(objectId);
        return _mapper.Map<StoreCustomerViewModel>(customer);
    }

    public async Task<IndividualCustomerViewModel> GetDetailIndividual(Guid objectId)
    {
        var customer = await GetCustomer(objectId);
        return _mapper.Map<IndividualCustomerViewModel>(customer);
    }

    private async Task<Infrastructure.Api.Customer.Handlers.GetCustomer.Customer> GetCustomer(Guid objectId)
    {
        _logger.LogInformation("Getting customer");
        var customer = await _mediator.Send(new GetCustomerQuery(objectId));
        _logger.LogInformation("Retrieved customer");
        Guard.Against.Null(customer, _logger);

        return customer!;
    }

    public async Task<List<Territory>> GetTerritories()
    {
        _logger.LogInformation("Send GetTerritories query");
        var territories = await _mediator.Send(new GetTerritoriesQuery());
        _logger.LogInformation("Received territories");

        return territories;
    }       

    public List<SelectListItem> GetCustomerTypes()
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

    public async Task<List<SalesPerson>> GetSalesPersons(string? territory)
    {
        _logger.LogInformation("Send GetSalesPersons query");
        var salesPersons = await _mediator.Send(new GetSalesPersonsQuery(territory));
        _logger.LogInformation("Received sales persons");

        return salesPersons;
    }

    public async Task<List<AddressType>> GetAddressTypes()
    {
        _logger.LogInformation("Send GetAddressTypes query");
        var addressTypes = await _mediator.Send(new GetAddressTypesQuery());
        _logger.LogInformation("Received address types");

        return addressTypes;
    }

    public async Task<List<CountryRegion>> GetCountries()
    {
        _logger.LogInformation("GetCountries called.");
        var countries = await _mediator.Send(new GetCountriesQuery());
        countries = countries.OrderBy(_ => _.Name).ToList();
        _logger.LogInformation("Received countries");

        return countries;
    }

    public async Task<StoreCustomerViewModel> UpdateStore(StoreCustomerViewModel viewModel)
    {
        _logger.LogInformation("Mapping CustomerViewModel to UpdateCustomerRequest");
        var customer = await _mediator.Send(new GetStoreCustomerQuery(viewModel.ObjectId));
        Guard.Against.Null(customer, _logger);

        var customerToUpdate = _mapper.Map<UpdateCustomer.StoreCustomer>(customer);
        customerToUpdate.Name = viewModel.Name;
        customerToUpdate.Territory = viewModel.Territory;
        customerToUpdate.SalesPerson = viewModel.SalesPerson;

        _logger.LogInformation("Calling Customer API to update customer");
        await _mediator.Send(new UpdateCustomer.UpdateCustomerCommand(customerToUpdate));
        _logger.LogInformation("Customer successfully updated");

        return await GetDetailStore(viewModel.ObjectId);
    }

    public async Task<IndividualCustomerViewModel> UpdateIndividual(IndividualCustomerViewModel viewModel)
    {
        var customer = await _mediator.Send(new GetIndividualCustomerQuery(viewModel.ObjectId));
        Guard.Against.Null(customer, _logger);

        var customerToUpdate = _mapper.Map<UpdateCustomer.IndividualCustomer>(customer);
        customerToUpdate.Person.Name = _mapper.Map<NameFactory>(viewModel.Person!.Name);

        _logger.LogInformation("Calling Customer API to update customer");
        await _mediator.Send(new UpdateCustomer.UpdateCustomerCommand(customerToUpdate));
        _logger.LogInformation("Customer successfully updated");

        return await GetDetailIndividual(viewModel.ObjectId);
    }

    public async Task<T> AddAddress<T>(EditCustomerAddressViewModel viewModel)
    {
        _logger.LogInformation("AddAddress called");

        _logger.LogInformation("Getting customer {ObjectId}", viewModel.CustomerId);
        var customer = await _mediator.Send(new GetCustomerQuery(viewModel.CustomerId));
        _logger.LogInformation("Retrieved customer {@Customer}", customer);
        Guard.Against.Null(customer, _logger);

        var customerToUpdate = _mapper.Map<UpdateCustomer.Customer>(customer);
        Guard.Against.Null(customerToUpdate, _logger);
        var newAddress = _mapper.Map<UpdateCustomer.CustomerAddress>(viewModel);
        newAddress.ObjectId = Guid.NewGuid();
        newAddress.Address!.ObjectId = Guid.NewGuid();
        customerToUpdate.Addresses?.Add(newAddress);

        _logger.LogInformation("Updating customer {@Customer}", customer);
        await _mediator.Send(new UpdateCustomer.UpdateCustomerCommand(customerToUpdate));
        _logger.LogInformation("Customer updated successfully");

        customer = await _mediator.Send(new GetCustomerQuery(viewModel.CustomerId));
        return _mapper.Map<T>(customer);
    }

    public async Task<T> UpdateAddress<T>(EditCustomerAddressViewModel viewModel)
    {
        _logger.LogInformation("UpdateAddress called");

        _logger.LogInformation("Getting customer {ObjectId}", viewModel.CustomerId);
        var customer = await _mediator.Send(new GetCustomerQuery(viewModel.CustomerId));
        _logger.LogInformation("Retrieved customer {@Customer}", customer);
        Guard.Against.Null(customer, _logger);

        var customerToUpdate = _mapper.Map<UpdateCustomer.Customer>(customer);
        Guard.Against.Null(customerToUpdate, _logger);
        var addressToUpdate = customerToUpdate.Addresses?.SingleOrDefault(_ => _?.ObjectId == viewModel.ObjectId);
        Guard.Against.Null(addressToUpdate, _logger);
        _mapper.Map(viewModel, addressToUpdate);

        _logger.LogInformation("Updating customer {@Customer}", customer);
        await _mediator.Send(new UpdateCustomer.UpdateCustomerCommand(customerToUpdate));
        _logger.LogInformation("Customer updated successfully");

        customer = await _mediator.Send(new GetCustomerQuery(viewModel.CustomerId));
        return _mapper.Map<T>(customer);
    }

    public async Task<T> DeleteAddress<T>(Guid customerId, Guid objectId)
    {
        _logger.LogInformation("DeleteAddress called");

        _logger.LogInformation("Getting customer {ObjectId}", customerId);
        var customer = await _mediator.Send(new GetCustomerQuery(customerId));
        _logger.LogInformation("Retrieved customer {@Customer}", customer);
        Guard.Against.Null(customer, _logger);

        var customerToUpdate = _mapper.Map<UpdateCustomer.Customer>(customer);
        Guard.Against.Null(customerToUpdate, _logger);
        var addressToDelete = customerToUpdate.Addresses?.FirstOrDefault(_ => _?.ObjectId == objectId);
        Guard.Against.Null(addressToDelete, _logger);
        customerToUpdate.Addresses?.Remove(addressToDelete);

        _logger.LogInformation("Updating customer {@Customer}", customer);
        await _mediator.Send(new UpdateCustomer.UpdateCustomerCommand(customerToUpdate));
        _logger.LogInformation("Customer updated successfully");

        customer = await _mediator.Send(new GetCustomerQuery(customerId));
        return _mapper.Map<T>(customer);
    }

    public async Task<List<StateProvince>> GetStatesProvinces(string countryRegionCode)
    {
        _logger.LogInformation("GetStatesProvinces called.");
        var statesProvinces = await _mediator.Send(new GetStatesProvincesQuery(countryRegionCode));
        statesProvinces = statesProvinces.OrderBy(_ => _.Name).ToList();
        _logger.LogInformation("Received states/provinces");

        return statesProvinces;
    }

    public async Task<IEnumerable<StateProvince>?> GetStatesProvincesJson(string? country)
    {
        var statesProvinces = await _mediator.Send(new GetStatesProvincesQuery(country));
        statesProvinces = statesProvinces.OrderBy(_ => _.Name).ToList();
        return statesProvinces;
    }

    public async Task<List<ContactType>> GetContactTypes()
    {
        _logger.LogInformation("GetContactTypes called.");
        var contactTypes = await _mediator.Send(new GetContactTypesQuery());
        Guard.Against.Null(contactTypes, _logger);

        return contactTypes;
    }

    public List<string> GetPhoneNumberTypes()
    {
        return new List<string> { "Cell", "Home", "Work" };
    }

    public async Task<StoreCustomerContactViewModel> GetCustomerContact(Guid customerId, Guid objectId)
    {
        _logger.LogInformation("GetCustomerContact called");
        var customer = await _mediator.Send(new GetStoreCustomerQuery(customerId));
        Guard.Against.Null(customer, _logger);

        var contact = customer!.Contacts.FirstOrDefault(_ =>
            _.ObjectId == objectId
        );
        Guard.Against.Null(contact, _logger);

        return _mapper.Map<StoreCustomerContactViewModel>(contact);
    }

    public async Task AddContact(StoreCustomerContactViewModel viewModel)
    {
        _logger.LogInformation("AddContact called");

        _logger.LogInformation("Getting customer {ObjectId}", viewModel.ObjectId);
        var customer = await _mediator.Send(new GetStoreCustomerQuery(viewModel.ObjectId));
        _logger.LogInformation("Retrieved customer {@Customer}", customer);
        Guard.Against.Null(customer, _logger);

        var customerToUpdate = _mapper.Map<UpdateCustomer.StoreCustomer>(customer);
        var contactToAdd = _mapper.Map<UpdateCustomer.StoreCustomerContact>(viewModel);
        customerToUpdate.Contacts?.Add(contactToAdd);

        _logger.LogInformation("Updating customer {@Customer}", customer);
        await _mediator.Send(new UpdateCustomer.UpdateCustomerCommand(customerToUpdate));
        _logger.LogInformation("Customer updated successfully");
    }

    public async Task<StoreCustomerContactViewModel> UpdateContact(Guid customerId, EditStoreCustomerContactViewModel viewModel)
    {
        _logger.LogInformation("UpdateContact called");

        _logger.LogInformation("Getting customer", customerId);
        var customer = await _mediator.Send(new GetStoreCustomerQuery(customerId));
        _logger.LogInformation("Retrieved customer", customer);
        Guard.Against.Null(customer, _logger);

        var customerToUpdate = _mapper.Map<UpdateCustomer.StoreCustomer>(customer);
        Guard.Against.Null(customerToUpdate, _logger);
        var contact = customerToUpdate.Contacts!.FirstOrDefault(_ => _!.ObjectId == viewModel.CustomerContact!.ObjectId);
        Guard.Against.Null(contact, _logger);
        _mapper.Map(viewModel.CustomerContact, contact);

        _logger.LogInformation("Updating customer {@Customer}", customer);
        await _mediator.Send(new UpdateCustomer.UpdateCustomerCommand(customerToUpdate));
        _logger.LogInformation("Customer updated successfully");

        return _mapper.Map<StoreCustomerContactViewModel>(contact);
    }

    public async Task<StoreCustomerViewModel> DeleteContact(Guid customerId, Guid objectId)
    {
        _logger.LogInformation("DeleteContact called");

        _logger.LogInformation("Getting customer", customerId);
        var customer = await _mediator.Send(new GetStoreCustomerQuery(customerId));
        _logger.LogInformation("Retrieved customer", customer);
        Guard.Against.Null(customer, _logger);

        var customerToUpdate = _mapper.Map<UpdateCustomer.StoreCustomer>(customer);
        var contact = customerToUpdate.Contacts!.FirstOrDefault(_ => _!.ObjectId == objectId);
        Guard.Against.Null(contact, _logger);
        customerToUpdate.Contacts?.Remove(contact);

        _logger.LogInformation("Updating customer {@Customer}", customer);
        await _mediator.Send(new UpdateCustomer.UpdateCustomerCommand(customerToUpdate));
        _logger.LogInformation("Customer updated successfully");

        return await GetDetailStore(customerId);
    }

    public async Task DeleteContactEmailAddress(Guid objectId, string? contactName, string? emailAddress)
    {
        _logger.LogInformation("DeleteContactEmailAddress called");

        _logger.LogInformation("Getting customer {ObjectId}", objectId);
        var customer = await _mediator.Send(new GetStoreCustomerQuery(objectId));
        _logger.LogInformation("Retrieved customer {@Customer}", customer);
        Guard.Against.Null(customer, _logger);

        var customerToUpdate = _mapper.Map<UpdateCustomer.StoreCustomer>(customer);
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
        await _mediator.Send(new UpdateCustomer.UpdateCustomerCommand(customerToUpdate!));
        _logger.LogInformation("Customer updated successfully");
    }

    public async Task DeleteIndividualCustomerEmailAddress(Guid objectId, string? emailAddress)
    {
        _logger.LogInformation("DeleteContactEmailAddress called");

        _logger.LogInformation("Getting customer {ObjectId}", objectId);
        var customer = await _mediator.Send(new GetIndividualCustomerQuery(objectId));
        _logger.LogInformation("Retrieved customer {@Customer}", customer);
        Guard.Against.Null(customer, _logger);

        var customerToUpdate = _mapper.Map<UpdateCustomer.IndividualCustomer>(customer);
        Guard.Against.Null(customerToUpdate, _logger);

        var personEmailAddress = customerToUpdate.Person?.EmailAddresses?.FirstOrDefault(c =>
            c?.EmailAddress == emailAddress
        );
        Guard.Against.Null(personEmailAddress, _logger);

        customerToUpdate.Person?.EmailAddresses?.Remove(personEmailAddress);

        _logger.LogInformation("Updating customer {@Customer}", customer);
        await _mediator.Send(new UpdateCustomer.UpdateCustomerCommand(customerToUpdate));
        _logger.LogInformation("Customer updated successfully");
    }
}
