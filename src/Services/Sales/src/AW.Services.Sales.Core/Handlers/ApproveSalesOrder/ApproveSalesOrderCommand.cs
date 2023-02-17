using MediatR;

namespace AW.Services.Sales.Core.Handlers.ApproveSalesOrder
{
    public class ApproveSalesOrderCommand : IRequest<bool>
    {
        public string? SalesOrderNumber { get; set; }
    }
}
