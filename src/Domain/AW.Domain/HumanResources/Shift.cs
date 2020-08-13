using System;
using System.Collections.Generic;

namespace AW.Domain.HumanResources
{
    public class Shift : BaseEntity
    {        
        public string Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}