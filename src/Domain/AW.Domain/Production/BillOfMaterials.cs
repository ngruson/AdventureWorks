using System;

namespace AW.Domain.Production
{
    public class BillOfMaterials : BaseEntity
    {
        public int? ProductAssemblyID { get; set; }

        public int ComponentID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        
        public string UnitMeasureCode { get; set; }

        public short BOMLevel { get; set; }

        public decimal PerAssemblyQty { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Product Product { get; set; }

        public virtual Product Product1 { get; set; }

        public virtual UnitMeasure UnitMeasure { get; set; }
    }
}