using MediatR;

namespace AW.Services.Sales.Core.Handlers.UpdateSalesOrder
{
    public class UpdateSalesOrderCommand : IRequest<SalesOrderDto>
    {
        public SalesOrderDto SalesOrder { get; set; }
    }
}