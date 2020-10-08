using AW.UI.Web.Internal.ViewModels.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Interfaces
{
    public interface ICustomersViewModelService
    {
        Task<CustomersIndexViewModel> GetCustomers(int pageIndex, int pageSize, string territory, string customerType);
        Task<CustomerDetailViewModel> GetCustomer(string accountNumber);
        Task<EditStoreCustomerViewModel> GetStoreCustomerForEdit(string accountNumber);
        Task<EditIndividualCustomerViewModel> GetIndividualCustomerForEdit(string accountNumber);
        Task UpdateStore(CustomerViewModel viewModel);
        Task UpdateIndividual(CustomerViewModel viewModel);
        Task<EditCustomerAddressViewModel> AddAddress(string accountNumber, string customerName);
        Task AddAddress(EditCustomerAddressViewModel viewModel);
        Task<EditCustomerAddressViewModel> EditAddress(string accountNumber, string customerName,
            string addressType, string addressLine1, string addressLine2, string postalCode, string city, string stateProvinceCode);
        Task UpdateAddress(EditCustomerAddressViewModel viewModel);
        Task<IEnumerable<StateProvinceViewModel>> GetStateProvincesJson(string country);
    }
}