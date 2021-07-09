using System;

namespace AW.Services.Product.Core.Entities
{
    public class ProductDocument
    {
        public int ProductID { get; set; }
        public string DocumentNode { get; set; }
        public Document Document { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}