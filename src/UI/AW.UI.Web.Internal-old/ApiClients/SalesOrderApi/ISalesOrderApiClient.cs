using AW.UI.Web.Internal.ApiClients.SalesOrderApi.Models;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.ApiClients.SalesOrderApi
{
    public interface ISalesOrderApiClient
    {
        Task<SalesOrdersResult> GetSalesOrdersAsync(int pageIndex, int pageSize, string territory, CustomerType? customerType);
        Task<SalesOrder> GetSalesOrderAsync(string salesOrderNumber);
    }
}