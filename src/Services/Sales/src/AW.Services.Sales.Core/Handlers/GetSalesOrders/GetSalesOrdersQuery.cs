using MediatR;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrders
{
    public class GetSalesOrdersQuery : IRequest<GetSalesOrdersDto>
    {
        public GetSalesOrdersQuery()
        {
        }
        public GetSalesOrdersQuery(int pageIndex, int pageSize, string territory)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Territory = territory;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? Territory { get; set; }
    }
}
