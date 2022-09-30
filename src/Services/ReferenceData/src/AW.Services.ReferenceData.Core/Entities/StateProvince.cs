using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.ReferenceData.Core.Entities
{
    public class StateProvince : IAggregateRoot
    {
        public StateProvince(CountryRegion countryRegion, string name)
        {
            CountryRegion = countryRegion;
            Name = name;
        }
        private StateProvince() { }

        public int Id { get; set; }
        public string? StateProvinceCode { get; private set; }

        public CountryRegion? CountryRegion { get; private set; }

        public bool IsOnlyStateProvinceFlag { get; private set; }

        public string? Name { get; private set; }
    }
}