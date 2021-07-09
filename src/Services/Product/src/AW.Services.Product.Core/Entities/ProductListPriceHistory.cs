using System;

namespace AW.Services.Product.Core.Entities
{
    public class ProductListPriceHistory
    {
        public int ProductID { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal ListPrice { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}