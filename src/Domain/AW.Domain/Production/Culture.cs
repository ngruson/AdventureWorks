using System;

namespace AW.Domain.Production
{
    public class Culture : BaseEntity
    {
        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }        
    }
}