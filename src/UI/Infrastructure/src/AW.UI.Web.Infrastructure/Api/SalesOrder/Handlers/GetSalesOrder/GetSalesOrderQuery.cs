using MediatR;

namespace AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder
{
    public class GetSalesOrderQuery : IRequest<SalesOrder>
    {
        public GetSalesOrderQuery(string? salesOrderNumber)
        {
            SalesOrderNumber = salesOrderNumber;
        }

        public string? SalesOrderNumber { get; set; }
    }
}