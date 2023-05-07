using MediatR;

namespace AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders
{
    public class GetSalesOrdersQuery : IRequest<SalesOrdersResult>
    {
        public GetSalesOrdersQuery(int pageIndex, int pageSize, string? territory, CustomerType? customerType)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Territory = territory;
            CustomerType = customerType;
        }

        public int PageIndex { get; init; }
        public int PageSize { get; init; }
        public string? Territory { get; init; }
        public CustomerType? CustomerType { get; init; }
    }
}