using AW.Core.Domain.Purchasing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Purchasing
{
    public class ProductVendorConfiguration : IEntityTypeConfiguration<ProductVendor>
    {
        public void Configure(EntityTypeBuilder<ProductVendor> builder)
        {
            builder.ToTable("Purchasing.ProductVendor");
            builder.HasKey(pv => new { pv.ProductID, pv.BusinessEntityID });

            builder.Property(pv => pv.ProductID)
                .ValueGeneratedNever();

            builder.Property(pv => pv.BusinessEntityID)
                .ValueGeneratedNever();

            builder.Property(pv => pv.StandardPrice)
                .HasColumnType("money");

            builder.Property(pv => pv.LastReceiptCost)
                .HasColumnType("money");

            builder.Property(pv => pv.UnitMeasureCode)
                .IsRequired()
                .HasMaxLength(3);
        }
    }
}