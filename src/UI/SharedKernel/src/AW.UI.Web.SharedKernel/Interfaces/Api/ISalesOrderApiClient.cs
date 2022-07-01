using AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrders;

namespace AW.UI.Web.SharedKernel.Interfaces.Api
{
    public interface ISalesOrderApiClient
    {
        Task<SalesOrdersResult> GetSalesOrdersAsync(int pageIndex, int pageSize, string? territory, CustomerType? customerType);
        Task<SalesOrder.Handlers.GetSalesOrder.SalesOrder> GetSalesOrderAsync(string salesOrderNumber);
        Task<SalesOrder.Handlers.UpdateSalesOrder.SalesOrder> UpdateSalesOrderAsync(SalesOrder.Handlers.UpdateSalesOrder.SalesOrder salesOrder);
        Task ApproveSalesOrderAsync(string salesOrderNumber);
    }
}