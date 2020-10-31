using AW.Domain.Person;
using System;
using System.Collections.Generic;

namespace AW.Domain.Sales
{
    public partial class SalesTerritory
    {
        public virtual int Id { get; protected set; }
        public string Name { get; set; }

        public string CountryRegionCode { get; set; }

        public string Group { get; set; }

        public decimal SalesYTD { get; set; }

        public decimal SalesLastYear { get; set; }

        public decimal CostYTD { get; set; }

        public decimal CostLastYear { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual CountryRegion CountryRegion { get; set; }

        public virtual ICollection<StateProvince> StateProvinces { get; set; } = new List<StateProvince>();

        public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

        public virtual ICollection<SalesPerson> SalesPersons { get; set; } = new List<SalesPerson>();

        public virtual ICollection<SalesTerritoryHistory> SalesTerritoryHistory { get; set; } = new List<SalesTerritoryHistory>();
    }
}
