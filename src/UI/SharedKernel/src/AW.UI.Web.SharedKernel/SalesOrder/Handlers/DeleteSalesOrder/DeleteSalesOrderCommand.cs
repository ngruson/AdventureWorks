using MediatR;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.DeleteSalesOrder
{
    public class DeleteSalesOrderCommand : IRequest
    {
        public DeleteSalesOrderCommand(string? salesOrderNumber)
        {
            SalesOrderNumber = salesOrderNumber;
        }

        public string? SalesOrderNumber { get; init; }
    }
}