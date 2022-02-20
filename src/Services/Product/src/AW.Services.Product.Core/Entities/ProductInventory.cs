using System;

namespace AW.Services.Product.Core.Entities
{
    public class ProductInventory
    {
        private int ProductID { get; set; }

        private short LocationID { get; set; }

        public string Shelf { get; private set; }

        public byte Bin { get; private set; }

        public short Quantity { get; private set; }

        public Guid rowguid { get; private set; }

        public DateTime ModifiedDate { get; private set; }

        public virtual Location Location { get; private set; }
    }
}