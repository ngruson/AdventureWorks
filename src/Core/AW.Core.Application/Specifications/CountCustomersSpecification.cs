﻿using Ardalis.Specification;
using AW.Core.Domain.Sales;

namespace AW.Core.Application.Specifications
{
    public class CountCustomersSpecification : Specification<Domain.Sales.Customer>
    {
        public CountCustomersSpecification(CustomerType? customerType, string territory) : base()
        {
            Query
                .Where(c =>
                    (string.IsNullOrEmpty(territory) || c.SalesTerritory.Name == territory) &&
                    (!customerType.HasValue || (customerType == CustomerType.Individual ?
                        c.Person != null && c.Store == null
                        : customerType == CustomerType.Store && c.Store != null))
                );
        }
    }
}