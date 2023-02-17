using MediatR;

namespace AW.Services.Sales.Core.Handlers.CancelSalesOrder
{
    public class CancelSalesOrderCommand : IRequest<bool>
    {
        public string? SalesOrderNumber { get; set; }
    }
}
