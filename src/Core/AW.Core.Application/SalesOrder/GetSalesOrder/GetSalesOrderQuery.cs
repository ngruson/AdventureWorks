using MediatR;

namespace AW.Core.Application.SalesOrder.GetSalesOrder
{
    public class GetSalesOrderQuery : IRequest<SalesOrderDto>
    {
        public string SalesOrderNumber { get; set; }
    }
}