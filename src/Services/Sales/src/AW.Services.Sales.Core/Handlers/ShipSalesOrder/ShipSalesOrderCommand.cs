using MediatR;

namespace AW.Services.Sales.Core.Handlers.ShipSalesOrder
{
    public class ShipSalesOrderCommand : IRequest<bool>
    {
        public string? SalesOrderNumber { get; set; }
    }
}
