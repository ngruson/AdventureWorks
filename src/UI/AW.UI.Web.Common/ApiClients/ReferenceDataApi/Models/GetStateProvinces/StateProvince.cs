﻿namespace AW.UI.Web.Common.ApiClients.ReferenceDataApi.Models.GetStateProvinces
{
    public class StateProvince
    {
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }
        public bool IsOnlyStateProvinceFlag { get; set; }
        public string Name { get; set; }
    }
}