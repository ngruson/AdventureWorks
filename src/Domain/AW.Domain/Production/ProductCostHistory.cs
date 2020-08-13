using System;

namespace AW.Domain.Production
{
    public class ProductCostHistory : BaseEntity
    {
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal StandardCost { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}