using MediatR;

namespace AW.Services.Sales.Core.Handlers.DeleteSalesOrder
{
    public class DeleteSalesOrderCommand : IRequest
    {
        public string SalesOrderNumber { get; set; }
    }
}