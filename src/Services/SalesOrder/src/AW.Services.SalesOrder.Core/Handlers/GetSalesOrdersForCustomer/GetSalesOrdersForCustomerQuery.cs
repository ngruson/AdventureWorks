using MediatR;
using System.Collections.Generic;

namespace AW.Services.SalesOrder.Core.Handlers.GetSalesOrdersForCustomer
{
    public class GetSalesOrdersForCustomerQuery : IRequest<GetSalesOrdersDto>
    {
        public string CustomerNumber { get; set; }
    }
}