using MediatR;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrder
{
    public class GetSalesOrderQuery : IRequest<SalesOrderDto>
    {
        public string SalesOrderNumber { get; set; }
    }
}