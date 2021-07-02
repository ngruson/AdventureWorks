namespace AW.Services.ReferenceData.Application.UnitTests.TestBuilders
{
    public class TerritoryBuilder
    {
        private Domain.Territory territory = new();

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

        public Domain.Territory Build()
        {
            return territory;
        }

        public TerritoryBuilder WithTestValues()
        {
            territory = new Domain.Territory
            {
                Name = "Northwest",
                CountryRegionCode = "US",
                Group = "North America"
            };

            return this;
        }
    }
}