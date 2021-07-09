﻿using Ardalis.Specification;
using System.Linq;

namespace AW.Services.Customer.Core.Specifications
{
    public class GetCustomersAccountNumberSpecification : Specification<Entities.Customer, string>
    {
        public GetCustomersAccountNumberSpecification() : base()
        {
            Query.Select(c => c.AccountNumber);

        }
    }
}