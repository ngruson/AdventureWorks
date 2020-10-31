using AW.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class SalesOrderDetailConfiguration : IEntityTypeConfiguration<SalesOrderDetail>
    {
        public void Configure(EntityTypeBuilder<SalesOrderDetail> builder)
        {
            builder.ToTable("SalesOrderDetail", "Sales");
            builder.HasKey(sod => new { sod.SalesOrderID, sod.SalesOrderDetailID });

            builder.Property(sod => sod.SalesOrderID)
                .HasColumnName("SalesOrderID")
                .ValueGeneratedNever();

            builder.Property(sod => sod.CarrierTrackingNumber)
                .HasMaxLength(25);

            builder.Property(sod => sod.UnitPrice)
                .HasColumnType("money");

            builder.Property(sod => sod.UnitPriceDiscount)
                .HasColumnType("money");

            builder.Property(sod => sod.LineTotal)
                .HasColumnType("decimal(38,6)")
                .HasColumnType("numeric")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}