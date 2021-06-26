using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Customer.Persistence.EntityFrameworkCore.Configurations
{
    public class PersonEmailAddressConfiguration : IEntityTypeConfiguration<Domain.PersonEmailAddress>
    {
        public void Configure(EntityTypeBuilder<Domain.PersonEmailAddress> builder)
        {
            builder.ToTable("PersonEmailAddress");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("PersonEmailAddressID");
        }
    }
}