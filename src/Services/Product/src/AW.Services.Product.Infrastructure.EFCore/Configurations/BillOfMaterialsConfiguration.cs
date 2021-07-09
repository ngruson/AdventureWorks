using AW.Services.Product.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class BillOfMaterialsConfiguration : IEntityTypeConfiguration<BillOfMaterials>
    {
        public void Configure(EntityTypeBuilder<BillOfMaterials> builder)
        {
            builder.ToTable("BillOfMaterials");
            builder.HasKey(bom => bom.Id);

            builder.HasOne(bom => bom.ProductAssembly);
            builder.HasOne(bom => bom.Component);

            builder.Property(e => e.UnitMeasureCode)
                .IsFixedLength()
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(e => e.PerAssemblyQty)
                .HasColumnType("decimal(8,2)");
        }
    }
}