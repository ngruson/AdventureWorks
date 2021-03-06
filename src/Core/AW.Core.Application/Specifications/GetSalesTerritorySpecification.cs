﻿using Ardalis.Specification;

namespace AW.Core.Application.Specifications
{
    public class GetSalesTerritorySpecification : Specification<Domain.Sales.SalesTerritory>
    {
        public GetSalesTerritorySpecification(string name) : base()
        {
            Query
                .Where(st => st.Name == name);
        }
    }
}