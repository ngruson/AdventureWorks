using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Customer.Persistence.EntityFrameworkCore.Configurations
{
    public class SalesOrderConfiguration : IEntityTypeConfiguration<Domain.SalesOrder>
    {
        public void Configure(EntityTypeBuilder<Domain.SalesOrder> builder)
        {
            builder.ToTable("SalesOrder");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id)
                .HasColumnName("SalesOrderID");
        }
    }
}