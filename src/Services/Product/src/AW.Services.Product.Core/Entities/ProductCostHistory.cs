using System;

namespace AW.Services.Product.Core.Entities
{
    public class ProductCostHistory
    {
        private int Id { get; set; }
        public DateTime StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }

        public decimal StandardCost { get; private set; }

        public DateTime ModifiedDate { get; private set; }
    }
}