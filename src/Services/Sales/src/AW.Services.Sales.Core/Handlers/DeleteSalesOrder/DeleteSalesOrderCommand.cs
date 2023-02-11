using MediatR;

namespace AW.Services.Sales.Core.Handlers.DeleteSalesOrder
{
    public class DeleteSalesOrderCommand : IRequest
    {
        public DeleteSalesOrderCommand(string salesOrderNumber)
        {
            SalesOrderNumber = salesOrderNumber;
        }

        public string SalesOrderNumber { get; private init; }
    }
}