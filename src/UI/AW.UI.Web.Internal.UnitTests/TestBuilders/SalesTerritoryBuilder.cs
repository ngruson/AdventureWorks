using AW.Core.Abstractions.Api.SalesTerritoryApi.ListTerritories;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders
{
    public class SalesTerritoryBuilder
    {
        private Territory territory = new Territory();

        public SalesTerritoryBuilder CountryRegion(CountryRegion countryRegion)
        {
            territory.CountryRegion = countryRegion;
            return this;
        }

        public SalesTerritoryBuilder Name(string name)
        {
            territory.Name = name;
            return this;
        }

        public SalesTerritoryBuilder Group(string group)
        {
            territory.Group = group;
            return this;
        }

        public Territory Build()
        {
            return territory;
        }

        public SalesTerritoryBuilder WithTestValues()
        {
            territory = new Territory
            {
                CountryRegion = new CountryRegion
                {
                    CountryRegionCode = "US",
                    Name = "United States"
                },
                Name = "Northwest",
                Group = "North America"
            };

            return this;
        }
    }
}