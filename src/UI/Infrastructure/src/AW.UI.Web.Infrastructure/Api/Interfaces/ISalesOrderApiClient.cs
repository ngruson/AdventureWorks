using AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders;

namespace AW.UI.Web.Infrastructure.Api.Interfaces
{
    public interface ISalesOrderApiClient
    {
        Task<SalesOrdersResult?> GetSalesOrdersAsync(int pageIndex, int pageSize, string? territory, CustomerType? customerType);
        Task<SalesOrder.Handlers.GetSalesOrder.SalesOrder?> GetSalesOrderAsync(string? salesOrderNumber);
        Task<SalesOrder.Handlers.UpdateSalesOrder.SalesOrder?> UpdateSalesOrderAsync(SalesOrder.Handlers.UpdateSalesOrder.SalesOrder salesOrder);
        Task ApproveSalesOrderAsync(string salesOrderNumber);
        Task DuplicateSalesOrderAsync(string salesOrderNumber);
        Task DeleteSalesOrderAsync(string salesOrderNumber);
    }
}