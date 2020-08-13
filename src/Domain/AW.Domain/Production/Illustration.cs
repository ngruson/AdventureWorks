using System;

namespace AW.Domain.Production
{
    public class Illustration : BaseEntity
    {        
        public string Diagram { get; set; }

        public DateTime ModifiedDate { get; set; }        
    }
}