using AW.SharedKernel.Interfaces;
using System.Collections.Generic;

namespace AW.Services.ReferenceData.Core.Entities
{
    public class CountryRegion : IAggregateRoot
    {
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }
        public List<StateProvince> StateProvinces { get; set; } = new List<StateProvince>();
    }
}