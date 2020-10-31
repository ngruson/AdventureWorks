using System;

namespace AW.Domain.Sales
{
    public partial class SalesOrderHeaderSalesReason
    {
        public int SalesOrderID { get; set; }
        public int SalesReasonID { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual SalesReason SalesReason { get; set; }
    }
}