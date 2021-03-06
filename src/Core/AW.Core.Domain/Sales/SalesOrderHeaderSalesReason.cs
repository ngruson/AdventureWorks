using System;

namespace AW.Core.Domain.Sales
{
    public partial class SalesOrderHeaderSalesReason
    {
        public int SalesOrderID { get; set; }
        public int SalesReasonID { get; set; }

        public SalesOrderHeader SalesOrder { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual SalesReason SalesReason { get; set; }
    }
}