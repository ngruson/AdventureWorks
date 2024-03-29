﻿using System.Collections.Generic;

namespace AW.Services.ReferenceData.Domain
{
    public class CountryRegion
    {
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }
        public List<StateProvince> StateProvinces { get; set; } = new List<StateProvince>();
    }
}