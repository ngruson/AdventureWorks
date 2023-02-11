using MediatR;

namespace AW.Services.Sales.Core.Handlers.ShipSalesOrder
{
    public class ShipSalesOrderCommand : IRequest<bool>
    {
        public ShipSalesOrderCommand(string salesOrderNumber)
        {
            SalesOrderNumber = salesOrderNumber;
        }
        public string SalesOrderNumber { get; private init; }
    }
}