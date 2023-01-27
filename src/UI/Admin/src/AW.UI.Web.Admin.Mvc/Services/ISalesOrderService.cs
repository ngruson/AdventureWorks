using AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder;
using AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrders;
using System.Threading.Tasks;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface ISalesOrderService
    {
        Task<SalesOrderIndexViewModel> GetSalesOrders(int pageIndex, int pageSize, string territory, CustomerType? customerType);
        Task<SalesOrderDetailViewModel> GetSalesOrder(string salesOrderNumber);
        Task<ApproveSalesOrderViewModel> GetSalesOrderForApproval(string salesOrderNumber);
        Task UpdateSalesOrder(SalesOrderViewModel viewModel);
        Task UpdateSalesOrder(ApproveSalesOrderViewModel viewModel);
        Task ApproveSalesOrder(string salesOrderNumber);
        Task DuplicateSalesOrder(string salesOrderNumber);
        Task DeleteSalesOrder(string salesOrderNumber);
    }
}