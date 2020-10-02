using AW.Domain.HumanResources;
using System.Collections.Generic;

namespace AW.Domain.Sales
{

    public partial class SalesPerson : Employee
    {
        //public int? TerritoryID { get; set; }
        public virtual SalesTerritory SalesTerritory { get; set; }
        public int? SalesTerritoryID { get; set; }

        public decimal? SalesQuota { get; set; }

        public decimal Bonus { get; set; }

        public decimal CommissionPct { get; set; }

        public decimal SalesYTD { get; set; }

        public decimal SalesLastYear { get; set; }

        public virtual ICollection<SalesOrderHeader> SalesOrders { get; set; } = new List<SalesOrderHeader>();

        

        public virtual ICollection<SalesPersonQuotaHistory> SalesPersonQuotaHistory { get; set; } = new List<SalesPersonQuotaHistory>();

        public virtual ICollection<SalesTerritoryHistory> SalesTerritoryHistory { get; set; } = new List<SalesTerritoryHistory>();

        public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}