using AW.Core.Abstractions.Api.CustomerApi.ListCustomers;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.ListCustomers
{
    public class StateProvinceBuilder
    {
        private StateProvince stateProvince = new StateProvince();

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

        public StateProvinceBuilder Name(string name)
        {
            stateProvince.Name = name;
            return this;
        }

        public StateProvinceBuilder SalesTerritory(string salesTerritoryName)
        {
            stateProvince.SalesTerritoryName = salesTerritoryName;
            return this;
        }

        public StateProvince Build()
        {
            return stateProvince;
        }

        public StateProvinceBuilder WithTestValues()
        {
            stateProvince = new StateProvince
            {
                StateProvinceCode = "WA",
                CountryRegion = new CountryRegionBuilder().WithTestValues().Build(),
                Name = "Washington",
                SalesTerritoryName = "Northwest"
            };

            return this;
        }
    }
}