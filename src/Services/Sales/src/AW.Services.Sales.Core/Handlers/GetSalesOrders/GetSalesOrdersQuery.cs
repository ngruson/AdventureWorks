using MediatR;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrders
{
    public class GetSalesOrdersQuery : IRequest<GetSalesOrdersDto>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Territory { get; set; }
    }
}