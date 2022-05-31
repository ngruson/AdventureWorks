using AW.UI.Web.Infrastructure.ApiClients.SalesOrderApi.Models;
using System.Threading.Tasks;

namespace AW.UI.Web.Infrastructure.ApiClients.SalesOrderApi
{
    public interface ISalesOrderApiClient
    {
        Task<SalesOrdersResult> GetSalesOrdersAsync(int pageIndex, int pageSize, string territory, CustomerType? customerType);
        Task<SalesOrder> GetSalesOrderAsync(string salesOrderNumber);
        Task<SalesOrder> UpdateSalesOrderAsync(SalesOrder salesOrder);
        Task ApproveSalesOrderAsync(string salesOrderNumber);
    }
}