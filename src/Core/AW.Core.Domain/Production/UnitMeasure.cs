using System;

namespace AW.Core.Domain.Production
{
    public class UnitMeasure
    {
        public virtual string UnitMeasureCode { get; protected set; }
        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}