using System;

namespace AW.Services.Product.Core.Entities
{
    public class BillOfMaterials
    {
        private int Id { get; set; }
        public int? ProductAssemblyID { get; private set; }
        public virtual Product ProductAssembly { get; private set; }

        public int ComponentID { get; private set; }
        public virtual Product Component { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }
        
        public string UnitMeasureCode { get; private set; }

        public short BOMLevel { get; private set; }

        public decimal PerAssemblyQty { get; private set; }

        public DateTime ModifiedDate { get; private set; }

        public virtual UnitMeasure UnitMeasure { get; private set; }
    }
}