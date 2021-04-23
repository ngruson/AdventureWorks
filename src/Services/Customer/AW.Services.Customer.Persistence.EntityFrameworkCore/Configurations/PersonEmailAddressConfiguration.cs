using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class PersonEmailAddressConfiguration : IEntityTypeConfiguration<Customer.Domain.PersonEmailAddress>
    {
        public void Configure(EntityTypeBuilder<Customer.Domain.PersonEmailAddress> builder)
        {
            builder.ToTable("PersonEmailAddress");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("PersonEmailAddressID");
        }
    }
}