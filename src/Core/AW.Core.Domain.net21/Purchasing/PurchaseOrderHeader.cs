using AW.Core.Domain.HumanResources;
using System;
using System.Collections.Generic;

namespace AW.Core.Domain.Purchasing
{
    public partial class PurchaseOrderHeader
    {
        public int PurchaseOrderID { get; set; }

        public byte RevisionNumber { get; set; }

        public byte Status { get; set; }

        public int EmployeeID { get; set; }

        public int VendorID { get; set; }

        public int ShipMethodID { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ShipDate { get; set; }
        
        public decimal SubTotal { get; set; }
        
        public decimal TaxAmt { get; set; }
        
        public decimal Freight { get; set; }

        public decimal TotalDue { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetail { get; set; } = new List<PurchaseOrderDetail>();

        public virtual ShipMethod ShipMethod { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}