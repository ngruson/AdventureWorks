using System;

namespace AW.Domain.Production
{
    public class ProductDocument : BaseEntity
    {
        public string DocumentNode { get; set; }
        public Document Document { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}