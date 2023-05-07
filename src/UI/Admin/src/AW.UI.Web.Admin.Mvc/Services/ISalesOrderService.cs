using AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder;
using AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface ISalesOrderService
    {
        Task<SalesOrderIndexViewModel> GetSalesOrders(int pageIndex, int pageSize, string territory, CustomerType? customerType);
        Task<SalesOrderDetailViewModel> GetSalesOrderDetail(string salesOrderNumber);
        Task<ApproveSalesOrderViewModel> GetSalesOrderForApproval(string salesOrderNumber);
        Task UpdateSalesOrder(SalesOrderViewModel viewModel);
        Task UpdateSalesOrder(ApproveSalesOrderViewModel viewModel);
        Task UpdateOrderlines(UpdateOrderlinesViewModel viewModel);
        Task UpdateOrderInfo(UpdateOrderInfoViewModel viewModel);
        Task ApproveSalesOrder(string salesOrderNumber);
        Task DuplicateSalesOrder(string salesOrderNumber);
        Task DeleteSalesOrder(string salesOrderNumber);
        Task UpdateShipToAddress(UpdateAddressViewModel viewModel);
        Task UpdateBillToAddress(UpdateAddressViewModel viewModel);
    }
}