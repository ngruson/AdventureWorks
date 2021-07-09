using AW.Services.Customer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Customer.Infrastructure.EFCore.Configurations
{
    public class PersonEmailAddressConfiguration : IEntityTypeConfiguration<PersonEmailAddress>
    {
        public void Configure(EntityTypeBuilder<PersonEmailAddress> builder)
        {
            builder.ToTable("PersonEmailAddress");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("PersonEmailAddressID");
        }
    }
}