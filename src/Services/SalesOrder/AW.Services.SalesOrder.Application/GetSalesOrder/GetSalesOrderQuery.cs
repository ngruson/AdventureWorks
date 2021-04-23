using MediatR;

namespace AW.Services.SalesOrder.Application.GetSalesOrder
{
    public class GetSalesOrderQuery : IRequest<SalesOrderDto>
    {
        public string SalesOrderNumber { get; set; }
    }
}