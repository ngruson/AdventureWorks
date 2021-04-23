namespace AW.Services.ReferenceData.Application.UnitTests.TestBuilders
{
    public class CountryRegionBuilder
    {
        private Domain.CountryRegion countryRegion = new Domain.CountryRegion();

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

        public Domain.CountryRegion Build()
        {
            return countryRegion;
        }

        public CountryRegionBuilder WithTestValues()
        {
            countryRegion = new Domain.CountryRegion
            {
                CountryRegionCode = "US",
                Name = "United States"
            };

            return this;
        }
    }
}