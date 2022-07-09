using AW.Services.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AW.Services.ReferenceData.Core.Entities
{
    public class CountryRegion : IAggregateRoot
    {
        public CountryRegion(string countryRegionCode)
        {
            CountryRegionCode = countryRegionCode;
        }
        public string CountryRegionCode { get; private set; }
        public string Name { get; private set; }

        public IEnumerable<StateProvince> StatesProvinces => _statesProvinces.ToList();
        private readonly List<StateProvince> _statesProvinces = new();
    }
}