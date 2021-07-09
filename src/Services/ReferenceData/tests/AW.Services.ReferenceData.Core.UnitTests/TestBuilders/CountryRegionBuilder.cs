namespace AW.Services.ReferenceData.Core.UnitTests.TestBuilders
{
    public class CountryRegionBuilder
    {
        private Entities.CountryRegion countryRegion = new();

        public CountryRegionBuilder CountryRegionCode(string countryRegionCode)
        {
            countryRegion.CountryRegionCode = countryRegionCode;
            return this;
        }

        public CountryRegionBuilder Name(string name)
        {
            countryRegion.Name = name;
            return this;
        }

        public Entities.CountryRegion Build()
        {
            return countryRegion;
        }

        public CountryRegionBuilder WithTestValues()
        {
            countryRegion = new Entities.CountryRegion
            {
                CountryRegionCode = "US",
                Name = "United States"
            };

            return this;
        }
    }
}