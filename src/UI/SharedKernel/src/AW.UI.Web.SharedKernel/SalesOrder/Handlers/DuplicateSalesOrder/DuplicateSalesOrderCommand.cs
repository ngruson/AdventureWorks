using MediatR;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.DuplicateSalesOrder
{
    public class DuplicateSalesOrderCommand : IRequest
    {
        public DuplicateSalesOrderCommand(string? salesOrderNumber)
        {
            SalesOrderNumber = salesOrderNumber;
        }

        public string? SalesOrderNumber { get; init; }
    }
}