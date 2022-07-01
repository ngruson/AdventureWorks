using MediatR;

namespace AW.UI.Web.SharedKernel.SalesOrder.Handlers.UpdateSalesOrder
{
    public class UpdateSalesOrderCommand : IRequest
    {
        public UpdateSalesOrderCommand(SalesOrder? salesOrder)
        {
            SalesOrder = salesOrder;
        }

        public SalesOrder? SalesOrder { get; init; }
    }
}