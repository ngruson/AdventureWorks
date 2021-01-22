using AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.UpdateCustomerAddress
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
                Name = "Washington"
            };

            return this;
        }
    }
}