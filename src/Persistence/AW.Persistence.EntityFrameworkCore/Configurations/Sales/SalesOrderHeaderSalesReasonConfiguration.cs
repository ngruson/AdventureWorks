using AW.Core.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class SalesOrderHeaderSalesReasonConfiguration : IEntityTypeConfiguration<SalesOrderHeaderSalesReason>
    {
        public void Configure(EntityTypeBuilder<SalesOrderHeaderSalesReason> builder)
        {
            builder.ToTable("SalesOrderHeaderSalesReason", "Sales");
            builder.HasKey(sohsr => new { sohsr.SalesOrderID, sohsr.SalesReasonID });

            builder.Property(sohsr => sohsr.SalesOrderID)
                .HasColumnName("SalesOrderID")
                .ValueGeneratedNever();

            builder.Property(sohsr => sohsr.SalesReasonID)
                .ValueGeneratedNever();
        }
    }
}