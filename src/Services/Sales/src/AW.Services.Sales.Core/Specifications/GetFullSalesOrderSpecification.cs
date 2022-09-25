﻿using Ardalis.Specification;

namespace AW.Services.Sales.Core.Specifications
{
    public class GetFullSalesOrderSpecification : Specification<Entities.SalesOrder>, ISingleResultSpecification<Entities.SalesOrder>
    {
        public GetFullSalesOrderSpecification(string salesOrderNumber) : base()
        {
            Query.Include(_ => _.Customer);
            Query.Include("Customer.Person");
            Query.Include(_ => _.BillToAddress);
            Query.Include(_ => _.ShipToAddress);
            Query.Include(_ => _.OrderLines);
            Query.Include(_ => _.SalesPerson);
            Query.Include(_ => _.SalesReasons)
                .ThenInclude(_ => _.SalesReason);

            Query
                .Where(c => c.SalesOrderNumber == salesOrderNumber);
        }
    }
}