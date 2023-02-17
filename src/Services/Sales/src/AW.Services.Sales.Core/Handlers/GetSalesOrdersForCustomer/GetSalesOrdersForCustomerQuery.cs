﻿using MediatR;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrdersForCustomer
{
    public class GetSalesOrdersForCustomerQuery : IRequest<GetSalesOrdersDto>
    {
        public string? CustomerNumber { get; set; }
    }
}
