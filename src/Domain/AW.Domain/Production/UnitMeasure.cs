using System;

namespace AW.Domain.Production
{
    public class UnitMeasure : BaseEntity<string>
    {
        public string Name { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}