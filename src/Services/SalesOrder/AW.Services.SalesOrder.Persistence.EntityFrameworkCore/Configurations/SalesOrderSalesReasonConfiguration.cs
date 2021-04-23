using AW.Services.SalesOrder.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesOrder.Persistence.EntityFrameworkCore.Configurations
{
    public class SalesOrderSalesReasonConfiguration : IEntityTypeConfiguration<SalesOrderSalesReason>
    {
        public void Configure(EntityTypeBuilder<SalesOrderSalesReason> builder)
        {
            builder.ToTable("SalesOrderSalesReason");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasColumnName("SalesOrderSalesReasonID");
        }
    }
}