using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Customer.Persistence.EntityFrameworkCore.Configurations
{
    public class CustomerAddressConfiguration : IEntityTypeConfiguration<Customer.Domain.CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<Customer.Domain.CustomerAddress> builder)
        {
            builder.ToTable("CustomerAddress");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id)
                .HasColumnName("CustomerAddressID");
        }
    }
}