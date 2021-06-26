using AW.Services.Product.Domain;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Persistence.EntityFramework.Configurations
{
    public class BillOfMaterialsConfiguration : EntityTypeConfiguration<BillOfMaterials>
    {
        public BillOfMaterialsConfiguration()
        {
            ToTable("BillOfMaterials");
            HasKey(bom => bom.Id);

            HasOptional(bom => bom.ProductAssembly);
            HasRequired(bom => bom.Component);

            Property(e => e.UnitMeasureCode)
                .IsFixedLength()
                .IsRequired()
                .HasMaxLength(3);

            Property(e => e.PerAssemblyQty)
                .HasPrecision(8, 2);
        }
    }
}