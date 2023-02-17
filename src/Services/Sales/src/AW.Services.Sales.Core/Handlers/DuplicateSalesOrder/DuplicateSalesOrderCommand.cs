using MediatR;

namespace AW.Services.Sales.Core.Handlers.DuplicateSalesOrder
{
    public class DuplicateSalesOrderCommand : IRequest
    {
        public string? SalesOrderNumber { get; set; }
    }
}
