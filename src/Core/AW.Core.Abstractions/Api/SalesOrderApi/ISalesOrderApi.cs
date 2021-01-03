using AW.Core.Abstractions.Api.SalesOrderApi.GetSalesOrder;
using AW.Core.Abstractions.Api.SalesOrderApi.ListSalesOrders;
using System.Threading.Tasks;

namespace AW.Core.Abstractions.Api.SalesOrderApi
{
    public interface ISalesOrderApi
    {
        Task<ListSalesOrdersResponse> ListSalesOrdersAsync(ListSalesOrdersRequest request);
        Task<GetSalesOrderResponse> GetSalesOrderAsync(GetSalesOrderRequest request);
    }
}