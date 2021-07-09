using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetStateProvinces;

namespace AW.UI.Web.Infrastructure.UnitTests.TestBuilders.GetStateProvinces
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

        public StateProvince Build()
        {
            return stateProvince;
        }
    }
}