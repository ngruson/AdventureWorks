using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders.GetTerritories
{
    public class SalesTerritoryBuilder
    {
        private Territory territory = new Territory();

        public SalesTerritoryBuilder Name(string name)
        {
            territory.Name = name;
            return this;
        }

        public SalesTerritoryBuilder CountryRegion(string countryRegionCode)
        {
            territory.CountryRegionCode = countryRegionCode;
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
                CountryRegionCode = "US",
                Name = "Northwest",
                Group = "North America"
            };

            return this;
        }
    }
}