using AW.Core.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Sales
{
    public class SalesReasonConfiguration : IEntityTypeConfiguration<SalesReason>
    {
        public void Configure(EntityTypeBuilder<SalesReason> builder)
        {
            builder.ToTable("SalesReason", "Sales");

            builder.Property(sr => sr.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(sr => sr.ReasonType)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}