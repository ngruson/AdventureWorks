using System;

namespace AW.Core.Domain.Production
{
    public class Illustration
    {
        public virtual int Id { get; protected set; }
        public string Diagram { get; set; }

        public DateTime ModifiedDate { get; set; }        
    }
}