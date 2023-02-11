using MediatR;

namespace AW.Services.Sales.Core.Handlers.DuplicateSalesOrder
{
    public class DuplicateSalesOrderCommand : IRequest
    {
        public DuplicateSalesOrderCommand(string salesOrderNumber)
        {
            SalesOrderNumber = salesOrderNumber;
        }

        public string SalesOrderNumber { get; private init; }
    }
}