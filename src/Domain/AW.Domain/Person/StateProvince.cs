using AW.Domain.Sales;
using System;

namespace AW.Domain.Person
{
    public class StateProvince
    {
        public virtual int Id { get; protected set; }
        public string StateProvinceCode { get; set; }
        
        public string CountryRegionCode { get; set; }

        public bool IsOnlyStateProvinceFlag { get; set; }
        
        public string Name { get; set; }

        public virtual SalesTerritory SalesTerritory { get; set; }
        public int SalesTerritoryID { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual CountryRegion CountryRegion { get; set; }
    }
}