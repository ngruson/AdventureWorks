using AW.UI.Web.Common.ApiClients.CustomerApi.Models.GetCustomers;
using AW.UI.Web.Internal.ViewModels.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Services
{
    public interface ICustomerService
    {
        Task<CustomersIndexViewModel> GetCustomers(int pageIndex, int pageSize, string territory, CustomerType? customerType, string accountNumber);
        Task<CustomerDetailViewModel> GetCustomer(string accountNumber);
        Task<EditStoreCustomerViewModel> GetStoreCustomerForEdit(string accountNumber);
        Task<EditIndividualCustomerViewModel> GetIndividualCustomerForEdit(string accountNumber);
        Task UpdateStore(StoreCustomerViewModel viewModel);
        Task UpdateIndividual(IndividualCustomerViewModel viewModel);
        EditCustomerAddressViewModel AddAddress(string accountNumber, string customerName);
        Task AddAddress(EditCustomerAddressViewModel viewModel);
        Task<EditCustomerAddressViewModel> GetCustomerAddress(string accountNumber, string addressType);
        Task UpdateAddress(EditCustomerAddressViewModel viewModel);
        Task<IEnumerable<StateProvinceViewModel>> GetStatesProvincesJson(string country);
        Task<DeleteCustomerAddressViewModel> GetCustomerAddressForDelete(string accountNumber, string addressType);
        Task DeleteAddress(string accountNumber, string addressType);
        Task<EditCustomerContactViewModel> AddContact(string accountNumber, string customerName);
        Task AddContact(EditCustomerContactViewModel viewModel);
        Task<EditCustomerContactViewModel> GetCustomerContact(string accountNumber, string contactName, string contactType);
        Task UpdateContact(EditCustomerContactViewModel viewModel);
        Task<DeleteCustomerContactViewModel> GetCustomerContactForDelete(string accountNumber, string contactName, string contactType);
        Task DeleteContact(DeleteCustomerContactViewModel viewModel);
        EditEmailAddressViewModel AddEmailAddress(string accountNumber, string customerName);
        Task AddIndividualCustomerEmailAddress(EditEmailAddressViewModel viewModel);
        Task AddContactEmailAddress(EditEmailAddressViewModel viewModel);
        Task<DeleteIndividualCustomerEmailAddressViewModel> GetIndividualCustomerEmailAddressForDelete(string accountNumber, string emailAddress);
        Task<DeleteContactEmailAddressViewModel> GetContactEmailAddressForDelete(string accountNumber, string contactType, string contactName, string emailAddress);
        Task DeleteIndividualCustomerEmailAddress(DeleteIndividualCustomerEmailAddressViewModel viewModel);
        Task DeleteContactEmailAddress(DeleteContactEmailAddressViewModel viewModel);
    }
}