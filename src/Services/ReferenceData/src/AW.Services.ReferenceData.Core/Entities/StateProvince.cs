using AW.SharedKernel.Interfaces;

namespace AW.Services.ReferenceData.Core.Entities
{
    public class StateProvince : IAggregateRoot
    {
        public int Id { get; set; }
        public string StateProvinceCode { get; set; }

        public string CountryRegionCode { get; set; }

        public bool IsOnlyStateProvinceFlag { get; set; }

        public string Name { get; set; }
        public CountryRegion CountryRegion { get; set; }
    }
}