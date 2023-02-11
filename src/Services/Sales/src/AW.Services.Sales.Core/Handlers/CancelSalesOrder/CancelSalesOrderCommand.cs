using MediatR;

namespace AW.Services.Sales.Core.Handlers.CancelSalesOrder
{
    public class CancelSalesOrderCommand : IRequest<bool>
    {
        public CancelSalesOrderCommand(string salesOrderNumber)
        {
            SalesOrderNumber = salesOrderNumber;
        }

        public string SalesOrderNumber { get; private init; }
    }
}