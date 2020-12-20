using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders.GetCustomer
{
    public class StateProvinceBuilder
    {
        private StateProvince stateProvince = new StateProvince();

        public StateProvinceBuilder StateProvinceCode(string stateProvinceCode)
        {
            stateProvince.StateProvinceCode = stateProvinceCode;
            return this;
        }

        public StateProvinceBuilder Name(string name)
        {
            stateProvince.Name = name;
            return this;
        }

        public StateProvinceBuilder CountryRegion(CountryRegion countryRegion)
        {
            stateProvince.CountryRegion = countryRegion;
            return this;
        }

        public StateProvince Build()
        {
            return stateProvince;
        }
    }
}