﻿using AW.UI.Web.Common.ApiClients.ReferenceDataApi.Models.GetCountries;

namespace AW.UI.Web.Common.UnitTests.TestBuilders.GetCountries
{
    public class CountryBuilder
    {
        private CountryRegion country = new CountryRegion();

        public CountryBuilder CountryRegionCode(string countryRegionCode)
        {
            country.CountryRegionCode = countryRegionCode;
            return this;
        }

        public CountryBuilder Name(string name)
        {
            country.Name = name;
            return this;
        }

        public CountryRegion Build()
        {
            return country;
        }
    }
}