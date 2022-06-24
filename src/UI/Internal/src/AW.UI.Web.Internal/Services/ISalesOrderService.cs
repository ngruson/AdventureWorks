using AW.UI.Web.Infrastructure.ApiClients.SalesOrderApi.Models;
using AW.UI.Web.Internal.ViewModels.SalesOrder;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Interfaces
{
    public interface ISalesOrderService
    {
        Task<SalesOrderIndexViewModel> GetSalesOrders(int pageIndex, int pageSize, string territory, CustomerType? customerType);
        Task<SalesOrderDetailViewModel> GetSalesOrder(string salesOrderNumber);
        Task<ApproveSalesOrderViewModel> GetSalesOrderForApproval(string salesOrderNumber);
        Task UpdateSalesOrder(SalesOrderViewModel viewModel);
        Task UpdateSalesOrder(ApproveSalesOrderViewModel viewModel);
        Task ApproveSalesOrder(string salesOrderNumber);
    }
}