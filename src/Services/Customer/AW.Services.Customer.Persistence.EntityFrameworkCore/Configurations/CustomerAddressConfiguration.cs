using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Customer.Persistence.EntityFrameworkCore.Configurations
{
    public class CustomerAddressConfiguration : IEntityTypeConfiguration<Domain.CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<Domain.CustomerAddress> builder)
        {
            builder.ToTable("CustomerAddress");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id)
                .HasColumnName("CustomerAddressID");
        }
    }
}