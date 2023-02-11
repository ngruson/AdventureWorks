using MediatR;

namespace AW.Services.Sales.Core.Handlers.RejectSalesOrder
{
    public class RejectSalesOrderCommand : IRequest<bool>
    {
        public string? SalesOrderNumber { get; set; }
    }
}