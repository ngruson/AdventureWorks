using System;

namespace AW.Domain.Sales
{
    public class SalesPersonQuotaHistory : BaseEntity
    {
        public DateTime QuotaDate { get; set; }

        public decimal SalesQuota { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}