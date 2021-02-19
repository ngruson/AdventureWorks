using System;

namespace AW.Core.Domain.Production
{
    public class Culture
    {
        public virtual int Id { get; protected set; }
        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }        
    }
}