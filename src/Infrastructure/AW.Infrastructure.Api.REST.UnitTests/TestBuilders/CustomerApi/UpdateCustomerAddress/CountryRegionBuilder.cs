using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.UpdateCustomerAddress
{
    public class CountryRegionBuilder
    {
        private CountryRegion countryRegion = new CountryRegion();

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

        public CountryRegion Build()
        {
            return countryRegion;
        }

        public CountryRegionBuilder WithTestValues()
        {
            countryRegion = new CountryRegion
            {
                CountryRegionCode = "US",
                Name = "United States"
            };

            return this;
        }
    }
}