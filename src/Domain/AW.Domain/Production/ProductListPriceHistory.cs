using System;

namespace AW.Domain.Production
{
    public class ProductListPriceHistory : BaseEntity
    {
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal ListPrice { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}