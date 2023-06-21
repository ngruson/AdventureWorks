using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetAddressTypes;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetContactTypes;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetCountries;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetStatesProvinces;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetTerritories;
using AW.UI.Web.Infrastructure.Api.SalesPerson.Handlers.GetSalesPersons;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AW.UI.Web.Admin.Mvc.Services;

public interface ICustomerService
{
    Task<List<CustomerViewModel>> GetCustomers();
    Task<StoreCustomerViewModel> GetDetailStore(Guid objectId);
    Task<IndividualCustomerViewModel> GetDetailIndividual(Guid objectId);
    Task<StoreCustomerViewModel> UpdateStore(StoreCustomerViewModel viewModel);
    Task<IndividualCustomerViewModel> UpdateIndividual(IndividualCustomerViewModel viewModel);
    Task<T> AddAddress<T>(EditCustomerAddressViewModel viewModel);
    Task<T> UpdateAddress<T>(EditCustomerAddressViewModel viewModel);
    Task<T> DeleteAddress<T>(Guid customerId, Guid objectId);
    Task<IEnumerable<StateProvince>?> GetStatesProvincesJson(string? country);
    Task AddContact(StoreCustomerContactViewModel viewModel);
    Task<StoreCustomerContactViewModel> GetCustomerContact(Guid objectId, Guid contactId);
    Task<StoreCustomerContactViewModel> UpdateContact(Guid customerId, EditStoreCustomerContactViewModel viewModel);
    Task<StoreCustomerViewModel> DeleteContact(Guid customerId, Guid objectId);
    Task DeleteContactEmailAddress(Guid objectId, string? contactName, string? emailAddress);
    Task DeleteIndividualCustomerEmailAddress(Guid objectId, string? emailAddress);
    Task<List<AddressType>> GetAddressTypes();
    Task<List<ContactType>> GetContactTypes();
    Task<List<CountryRegion>> GetCountries();
    List<SelectListItem> GetCustomerTypes();
    List<string> GetPhoneNumberTypes();
    Task<List<Territory>> GetTerritories();
    Task<List<SalesPerson>> GetSalesPersons(string? territory);
    Task<List<StateProvince>> GetStatesProvinces(string countryRegionCode);
}
