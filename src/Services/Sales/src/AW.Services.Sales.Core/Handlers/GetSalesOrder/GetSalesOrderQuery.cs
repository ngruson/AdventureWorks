using MediatR;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrder
{
    public class GetSalesOrderQuery : IRequest<SalesOrderDto>
    {
        public GetSalesOrderQuery()
        {
        }
        public GetSalesOrderQuery(string salesOrderNumber)
        {
            SalesOrderNumber = salesOrderNumber;
        }

        public string SalesOrderNumber { get; set; }
    }
}