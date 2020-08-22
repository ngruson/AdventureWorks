using AW.Domain.Sales;
using MediatR;
using System.Collections.Generic;

namespace AW.Application.GetSalesOrders
{
    public class GetSalesOrdersQuery : IRequest<IEnumerable<SalesOrderDto>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public CustomerType? CustomerType { get; set; }
        public string Territory { get; set; }
    }
}