using MediatR;

namespace AW.Services.SalesOrder.Core.Handlers.GetSalesOrder
{
    public class GetSalesOrderQuery : IRequest<SalesOrderDto>
    {
        public string SalesOrderNumber { get; set; }
    }
}