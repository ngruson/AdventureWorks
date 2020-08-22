using AW.Domain.Sales;
using MediatR;

namespace AW.Application.CountSalesOrders
{
    public class CountSalesOrdersQuery : IRequest<int>
    {
        public CustomerType? CustomerType { get; set; }
        public string Territory { get; set; }
    }
}