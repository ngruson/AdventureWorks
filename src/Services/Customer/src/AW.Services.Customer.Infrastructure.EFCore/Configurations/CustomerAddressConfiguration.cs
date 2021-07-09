using AW.Services.Customer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Customer.Infrastructure.EFCore.Configurations
{
    public class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            builder.ToTable("CustomerAddress");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id)
                .HasColumnName("CustomerAddressID");
        }
    }
}