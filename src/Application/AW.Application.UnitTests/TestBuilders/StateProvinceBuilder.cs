using AW.Domain.Person;
using AW.Domain.Sales;
using System;

namespace AW.Application.UnitTests.TestBuilders
{
    public class StateProvinceBuilder
    {
        private Domain.Person.StateProvince stateProvince = new Domain.Person.StateProvince();

        public StateProvinceBuilder Id(int id)
        {
            stateProvince.Id = id;
            return this;
        }

        public StateProvinceBuilder StateProvinceCode(string stateProvinceCode)
        {
            stateProvince.StateProvinceCode = stateProvinceCode;
            return this;
        }

        public StateProvinceBuilder CountryRegion(CountryRegion countryRegion)
        {
            stateProvince.CountryRegion = countryRegion;
            return this;
        }

        public StateProvinceBuilder IsOnlyStateProvinceFlag(bool isOnlyStateProvinceFlag)
        {
            stateProvince.IsOnlyStateProvinceFlag = isOnlyStateProvinceFlag;
            return this;
        }

        public StateProvinceBuilder Name(string name)
        {
            stateProvince.Name = name;
            return this;
        }

        public StateProvinceBuilder SalesTerritory(Domain.Sales.SalesTerritory salesTerritory)
        {
            stateProvince.SalesTerritory = salesTerritory;
            return this;
        }

        public Domain.Person.StateProvince Build()
        {
            return stateProvince;
        }

        public StateProvinceBuilder WithTestValues()
        {
            stateProvince = new Domain.Person.StateProvince
            {
                Id = new Random().Next(),
                StateProvinceCode = "WA",
                CountryRegion = new CountryRegionBuilder().WithTestValues().Build(),
                IsOnlyStateProvinceFlag = false,
                Name = "Washington",
                SalesTerritory = new SalesTerritoryBuilder().WithTestValues().Build()
            };

            return this;
        }
    }
}