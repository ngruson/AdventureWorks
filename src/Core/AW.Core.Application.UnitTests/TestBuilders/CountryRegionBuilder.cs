namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class CountryRegionBuilder
    {
        private Domain.Person.CountryRegion countryRegion = new Domain.Person.CountryRegion();

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

        public Domain.Person.CountryRegion Build()
        {
            return countryRegion;
        }

        public CountryRegionBuilder WithTestValues()
        {
            countryRegion = new Domain.Person.CountryRegion
            {
                CountryRegionCode = "US",
                Name = "United States"
            };

            return this;
        }
    }
}