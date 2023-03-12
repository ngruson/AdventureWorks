using MediatR;

namespace AW.Services.Sales.Core.Handlers.UpdateSalesOrder
{
    public class UpdateSalesOrderCommand : IRequest<SalesOrder>
    {
        public UpdateSalesOrderCommand(SalesOrder salesOrder)
        {
            SalesOrder = salesOrder;
        }
        public SalesOrder SalesOrder { get; set; }
    }
}
