using MediatR;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.ApproveSalesOrder
{
    public class ApproveSalesOrderCommand : IRequest
    {
        public ApproveSalesOrderCommand(string? salesOrderNumber)
        {
            SalesOrderNumber = salesOrderNumber;
        }

        public string? SalesOrderNumber { get; init; }
    }
}