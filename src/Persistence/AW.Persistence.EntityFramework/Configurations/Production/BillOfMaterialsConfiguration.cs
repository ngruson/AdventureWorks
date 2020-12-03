using AW.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class BillOfMaterialsConfiguration : EntityTypeConfiguration<BillOfMaterials>
    {
        public BillOfMaterialsConfiguration()
        {
            ToTable("Production.BillOfMaterials");

            Property(e => e.UnitMeasureCode)
                .IsFixedLength()
                .IsRequired()
                .HasMaxLength(3);

            Property(e => e.PerAssemblyQty)
                .HasPrecision(8, 2);
        }
    }
}