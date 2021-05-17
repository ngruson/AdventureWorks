using AW.UI.Web.Common.ApiClients.SalesOrderApi.Models;
using System.Threading.Tasks;

namespace AW.UI.Web.Common.ApiClients.SalesOrderApi
{
    public interface ISalesOrderApiClient
    {
        Task<SalesOrdersResult> GetSalesOrdersAsync(int pageIndex, int pageSize, string territory, CustomerType? customerType);
        Task<SalesOrder> GetSalesOrderAsync(string salesOrderNumber);
    }
}