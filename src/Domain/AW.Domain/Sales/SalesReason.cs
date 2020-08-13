using System;

namespace AW.Domain.Sales
{
    public partial class SalesReason
    {
        public int SalesReasonID { get; set; }

        public string Name { get; set; }

        public string ReasonType { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}