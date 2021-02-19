using AW.Core.Domain.Person;
using System;

namespace AW.Core.Domain.Sales
{
    public partial class CountryRegionCurrency
    {
        public string CountryRegionCode { get; set; }

        public string CurrencyCode { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual StateProvince CountryRegion { get; set; }

        public virtual Currency Currency { get; set; }
    }
}