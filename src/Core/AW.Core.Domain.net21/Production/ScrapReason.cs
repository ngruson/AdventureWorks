using System;

namespace AW.Core.Domain.Production
{
    public class ScrapReason 
    {
        public virtual short Id { get; set; }

        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}