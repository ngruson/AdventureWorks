using MediatR;

namespace AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.DuplicateSalesOrder
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