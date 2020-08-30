using AW.Domain.Purchasing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Purchasing
{
    public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.ToTable("Purchasing.Vendor");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .ValueGeneratedNever();

            builder.Property(v => v.AccountNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.PurchasingWebServiceURL)
                .HasMaxLength(1024);
        }
    }
}