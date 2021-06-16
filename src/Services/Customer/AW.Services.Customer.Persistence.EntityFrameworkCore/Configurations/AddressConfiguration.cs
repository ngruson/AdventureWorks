using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Customer.Persistence.EntityFrameworkCore.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Customer.Domain.Address>
    {
        public void Configure(EntityTypeBuilder<Customer.Domain.Address> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id)
                .HasColumnName("AddressID");
        }
    }
}