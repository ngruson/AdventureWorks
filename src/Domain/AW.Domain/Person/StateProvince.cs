using AW.Domain.Sales;
using System;

namespace AW.Domain.Person
{
    public class StateProvince : BaseEntity
    {
        public string StateProvinceCode { get; set; }
        
        public string CountryRegionCode { get; set; }

        public bool IsOnlyStateProvinceFlag { get; set; }
        
        public string Name { get; set; }

        public int TerritoryID { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual CountryRegion CountryRegion { get; set; }

        public virtual SalesTerritory SalesTerritory { get; set; }
    }
}