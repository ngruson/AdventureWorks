﻿namespace AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress
{
    public class StateProvince
    {
        public string StateProvinceCode { get; set; }
        public string Name { get; set; }
        public CountryRegion CountryRegion { get; set; }
    }
}