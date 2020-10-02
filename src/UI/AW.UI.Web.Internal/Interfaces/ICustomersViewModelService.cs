using AW.UI.Web.Internal.ViewModels.Customer;
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
    }
}