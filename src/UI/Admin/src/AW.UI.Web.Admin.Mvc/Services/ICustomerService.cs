using AW.SharedKernel.Interfaces;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetStatesProvinces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface ICustomerService
    {
        Task<CustomersIndexViewModel> GetCustomers(int pageIndex, int pageSize, string? territory, CustomerType? customerType, string? accountNumber);
        Task<CustomerViewModel> GetCustomer(string? accountNumber);
        Task UpdateStore(StoreCustomerViewModel viewModel);
        Task UpdateIndividual(IndividualCustomerViewModel viewModel);
        Task AddAddress(CustomerAddressViewModel viewModel, string? accountNumber);
        Task UpdateAddress(CustomerAddressViewModel viewModel, string? accountNumber);
        IEnumerable<SelectListItem> GetPhoneNumberTypes();
        Task<IEnumerable<StateProvince>?> GetStatesProvincesJson(string? country);
        Task DeleteAddress(string? accountNumber, string? addressType);
        Task<StoreCustomerContactViewModel> AddContact(string? accountNumber, string? customerName);
        Task AddContact(StoreCustomerContactViewModel viewModel);
        Task<StoreCustomerContactViewModel> GetCustomerContact(string? accountNumber, string? contactName);
        Task UpdateContact(StoreCustomerContactViewModel viewModel);
        Task DeleteContact(string? accountNumber, string? contactName);
        Task DeleteContactEmailAddress(string? accountNumber, string? contactName, string? emailAddress);
        Task DeleteIndividualCustomerEmailAddress(string? accountNumber, string? emailAddress);
        Task<IEnumerable<SelectListItem>?> GetAddressTypes();
        Task<IEnumerable<SelectListItem>?> GetCountries();
        Task<IEnumerable<SelectListItem>?> GetTerritories();
        Task<IEnumerable<SelectListItem>?> GetSalesPersons(string territory);
        Task<IEnumerable<SelectListItem>?> GetStatesProvinces(string countryRegionCode);
    }
}