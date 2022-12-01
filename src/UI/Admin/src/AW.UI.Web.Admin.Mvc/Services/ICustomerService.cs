using AW.SharedKernel.Interfaces;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetStatesProvinces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface ICustomerService
    {
        Task<CustomersIndexViewModel> GetCustomers(int pageIndex, int pageSize, string territory, CustomerType? customerType, string accountNumber);
        Task<CustomerViewModel> GetCustomer(string accountNumber);
        Task<EditStoreCustomerViewModel> GetStoreCustomerForEdit(string accountNumber);
        Task<EditIndividualCustomerViewModel> GetIndividualCustomerForEdit(string accountNumber);
        Task UpdateStore(StoreCustomerViewModel viewModel);
        Task UpdateIndividual(IndividualCustomerViewModel viewModel);
        EditCustomerAddressViewModel AddAddress(string accountNumber, string customerName);
        Task AddAddress(EditCustomerAddressViewModel viewModel);
        Task<EditCustomerAddressViewModel> GetCustomerAddress(string accountNumber, string addressType);
        Task UpdateAddress(EditCustomerAddressViewModel viewModel);
        Task<IEnumerable<StateProvince>> GetStatesProvincesJson(string country);
        Task<DeleteCustomerAddressViewModel> GetCustomerAddressForDelete(string accountNumber, string addressType);
        Task DeleteAddress(string accountNumber, string addressType);
        Task<StoreCustomerContactViewModel> AddContact(string accountNumber, string customerName);
        Task AddContact(StoreCustomerContactViewModel viewModel);
        Task<StoreCustomerContactViewModel> GetCustomerContact(string accountNumber, string contactName);
        Task UpdateContact(StoreCustomerContactViewModel viewModel);
        Task DeleteContact(string accountNumber, string contactName);
        EditEmailAddressViewModel AddEmailAddress(string accountNumber, string customerName);
        Task AddIndividualCustomerEmailAddress(EditEmailAddressViewModel viewModel);
        Task AddContactEmailAddress(EditEmailAddressViewModel viewModel);
        Task<DeleteIndividualCustomerEmailAddressViewModel> GetIndividualCustomerEmailAddressForDelete(string accountNumber, string emailAddress);
        Task DeleteIndividualCustomerEmailAddress(DeleteIndividualCustomerEmailAddressViewModel viewModel);
        Task DeleteContactEmailAddress(string accountNumber, string contactName, string emailAddress);
        Task<IEnumerable<SelectListItem>> GetTerritories(bool edit);
        Task<IEnumerable<SelectListItem>> GetSalesPersons(string territory);
    }
}