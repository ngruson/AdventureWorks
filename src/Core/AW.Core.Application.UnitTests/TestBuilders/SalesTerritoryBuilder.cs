using System;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class SalesTerritoryBuilder
    {
        private Domain.Sales.SalesTerritory salesTerritory = new Domain.Sales.SalesTerritory();

        public SalesTerritoryBuilder Id(int id)
        {
            salesTerritory.Id = id;
            return this;
        }

        public SalesTerritoryBuilder Name(string name)
        {
            salesTerritory.Name = name;
            return this;
        }

        public SalesTerritoryBuilder CountryRegion(Domain.Person.CountryRegion countryRegion)
        {
            salesTerritory.CountryRegion = countryRegion;
            return this;
        }

        public SalesTerritoryBuilder Group(string group)
        {
            salesTerritory.Group = group;
            return this;
        }

        public Domain.Sales.SalesTerritory Build()
        {
            return salesTerritory;
        }

        public SalesTerritoryBuilder WithTestValues()
        {
            salesTerritory = new Domain.Sales.SalesTerritory
            {
                Id = new Random().Next(),
                Name = "Northwest",
                CountryRegion = new CountryRegionBuilder().WithTestValues().Build(),
                Group = "North America"
            };

            return this;
        }
    }
}