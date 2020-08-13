using System;

namespace AW.Domain.HumanResources
{    
    public partial class JobCandidate : BaseEntity
    {
        public int? BusinessEntityID { get; set; }
        
        public string Resume { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Employee Employee { get; set; }
    }
}