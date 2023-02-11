using MediatR;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrders
{
    public class GetSalesOrdersQuery : IRequest<GetSalesOrdersDto>
    {
        public GetSalesOrdersQuery(int pageIndex, int pageSize, string territory)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Territory = territory;
        }

        public int PageIndex { get; private init; }
        public int PageSize { get; private init; }
        public string Territory { get; private init; }
    }
}