using System;

namespace AW.Domain.Production
{
    public class BillOfMaterials
    {
        public virtual int Id { get; protected set; }
        public int? ProductAssemblyID { get; set; }
        public virtual Product ProductAssembly { get; set; }

        public int ComponentID { get; set; }
        public virtual Product Component { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        
        public string UnitMeasureCode { get; set; }

        public short BOMLevel { get; set; }

        public decimal PerAssemblyQty { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual UnitMeasure UnitMeasure { get; set; }
    }
}