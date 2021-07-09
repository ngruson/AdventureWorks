using System;

namespace AW.Services.Product.Core.Entities
{
    public class ProductInventory
    {
        public int ProductID { get; set; }

        public short LocationID { get; set; }

        public string Shelf { get; set; }

        public byte Bin { get; set; }

        public short Quantity { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Location Location { get; set; }
    }
}