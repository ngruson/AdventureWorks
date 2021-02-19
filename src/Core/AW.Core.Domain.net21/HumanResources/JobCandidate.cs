using System;

namespace AW.Core.Domain.HumanResources
{    
    public partial class JobCandidate
    {
        public virtual int Id { get; protected set; }
        public int? BusinessEntityID { get; set; }
        
        public string Resume { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Employee Employee { get; set; }
    }
}