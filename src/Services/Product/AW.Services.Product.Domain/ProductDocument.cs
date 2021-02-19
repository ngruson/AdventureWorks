using System;

namespace AW.Services.Product.Domain
{
    public class ProductDocument
    {
        public int ProductID { get; set; }
        public string DocumentNode { get; set; }
        public Document Document { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}