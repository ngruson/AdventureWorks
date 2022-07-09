using System;

namespace AW.Services.Product.Core.Entities
{
    public class ProductListPriceHistory
    {
        public int ProductID { get; set; }
        public DateTime StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }

        public decimal ListPrice { get; private set; }

        public DateTime ModifiedDate { get; private set; }
    }
}