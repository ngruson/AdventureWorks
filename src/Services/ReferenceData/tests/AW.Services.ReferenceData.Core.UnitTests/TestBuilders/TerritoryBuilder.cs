namespace AW.Services.ReferenceData.Core.UnitTests.TestBuilders
{
    public class TerritoryBuilder
    {
        private Entities.Territory territory = new();

        public TerritoryBuilder Name(string name)
        {
            territory.Name = name;
            return this;
        }

        public TerritoryBuilder CountryRegionCode(string countryRegionCode)
        {
            territory.CountryRegionCode = countryRegionCode;
            return this;
        }

        public TerritoryBuilder Group(string group)
        {
            territory.Group = group;
            return this;
        }

        public Entities.Territory Build()
        {
            return territory;
        }

        public TerritoryBuilder WithTestValues()
        {
            territory = new Entities.Territory
            {
                Name = "Northwest",
                CountryRegionCode = "US",
                Group = "North America"
            };

            return this;
        }
    }
}