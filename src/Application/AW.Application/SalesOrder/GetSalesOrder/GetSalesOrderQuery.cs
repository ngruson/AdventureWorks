using MediatR;

namespace AW.Application.SalesOrder.GetSalesOrder
{
    public class GetSalesOrderQuery : IRequest<SalesOrderDto>
    {
        public string SalesOrderNumber { get; set; }
    }
}