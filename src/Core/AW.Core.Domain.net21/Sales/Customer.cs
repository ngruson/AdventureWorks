using System.Collections.Generic;

namespace AW.Core.Domain.Sales
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }

        public virtual Person.Person Person { get; set; }
        public int? PersonID { get; set; }

        public virtual Store Store { get; set; }
        public int? StoreID { get; set; }

        public virtual SalesTerritory SalesTerritory { get; set; }
        public int? SalesTerritoryID { get; set; }

        public virtual ICollection<SalesOrderHeader> SalesOrders { get; set; } = new List<SalesOrderHeader>();
    }
}