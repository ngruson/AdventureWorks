using AW.SharedKernel.Interfaces;

namespace AW.Services.ReferenceData.Core.Entities
{
    public class StateProvince : IAggregateRoot
    {
        public StateProvince(string countryRegionCode, string name)
        {
            CountryRegionCode = countryRegionCode;
            Name = name;
        }

        private int Id { get; set; }
        public string StateProvinceCode { get; private set; }

        public string CountryRegionCode { get; private set; }

        public bool IsOnlyStateProvinceFlag { get; private set; }

        public string Name { get; private set; }
        public CountryRegion CountryRegion { get; private set; }
    }
}