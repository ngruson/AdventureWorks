using AW.UI.Web.Internal.CustomerService;
using AW.UI.Web.Internal.SalesTerritoryService;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders
{
    public class SalesTerritoryBuilder
    {
        private TerritoryDto territory = new TerritoryDto();

        public SalesTerritoryBuilder CountryRegion(CountryRegionDto countryRegion)
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

        public TerritoryDto Build()
        {
            return territory;
        }

        public SalesTerritoryBuilder WithTestValues()
        {
            territory = new TerritoryDto
            {
                CountryRegion = new CountryRegionDto
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
