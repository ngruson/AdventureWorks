using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.SalesOrder.Persistence.EntityFrameworkCore.Configurations
{
    public class SalesOrderConfiguration : IEntityTypeConfiguration<Domain.SalesOrder>
    {
        public void Configure(EntityTypeBuilder<Domain.SalesOrder> builder)
        {
            builder.ToTable("SalesOrder");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("SalesOrderID");

            builder.Ignore(p => p.SubTotal);
            builder.Ignore(p => p.TaxAmt);
            builder.Ignore(p => p.Freight);
            builder.Ignore(p => p.TotalDue);
        }
    }
}