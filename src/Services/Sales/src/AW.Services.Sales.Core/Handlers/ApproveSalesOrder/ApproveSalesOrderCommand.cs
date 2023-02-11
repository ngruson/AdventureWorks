using MediatR;

namespace AW.Services.Sales.Core.Handlers.ApproveSalesOrder
{
    public class ApproveSalesOrderCommand : IRequest<bool>
    {
        public ApproveSalesOrderCommand(string salesOrderNumber)
        {
            SalesOrderNumber = salesOrderNumber;
        }

        public string SalesOrderNumber { get; private init; }
    }
}