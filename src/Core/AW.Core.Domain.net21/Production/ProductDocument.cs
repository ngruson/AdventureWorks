using System;

namespace AW.Core.Domain.Production
{
    public class ProductDocument
    {
        public int ProductID { get; set; }
        public string DocumentNode { get; set; }
        public Document Document { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}