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
            builder.HasKey(_ => _.Id);

            builder.HasOne(_ => _.ProductAssembly);
            builder.HasOne(_ => _.Component);

            builder.Property(_ => _.UnitMeasureCode)
                .IsFixedLength()
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(_ => _.PerAssemblyQty)
                .HasColumnType("decimal(8,2)");
        }
    }
}