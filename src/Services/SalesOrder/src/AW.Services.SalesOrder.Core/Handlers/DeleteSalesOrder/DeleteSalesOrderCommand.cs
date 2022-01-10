using MediatR;

namespace AW.Services.SalesOrder.Core.Handlers.DeleteSalesOrder
{
    public class DeleteSalesOrderCommand : IRequest
    {
        public string SalesOrderNumber { get; set; }
    }
}