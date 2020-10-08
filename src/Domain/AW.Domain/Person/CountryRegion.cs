using System;
using System.Collections.Generic;

namespace AW.Domain.Person
{
    public partial class CountryRegion
    {
        public string CountryRegionCode { get; set; }

        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }

        public ICollection<StateProvince> StateProvinces { get; set; } = new List<StateProvince>();
    }
}