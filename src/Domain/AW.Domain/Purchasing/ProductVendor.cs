using AW.Domain.Production;
using System;

namespace AW.Domain.Purchasing
{
    public class ProductVendor : BaseEntity
    {
        public int ProductID { get; set; }

        public int BusinessEntityID { get; set; }

        public int AverageLeadTime { get; set; }

        public decimal StandardPrice { get; set; }

        public decimal? LastReceiptCost { get; set; }

        public DateTime? LastReceiptDate { get; set; }

        public int MinOrderQty { get; set; }

        public int MaxOrderQty { get; set; }

        public int? OnOrderQty { get; set; }

        public string UnitMeasureCode { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Product Product { get; set; }

        public virtual UnitMeasure UnitMeasure { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}