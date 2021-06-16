using MediatR;

namespace AW.Services.SalesOrder.Application.GetSalesOrders
{
    public class GetSalesOrdersQuery : IRequest<GetSalesOrdersDto>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Territory { get; set; }
    }
}