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
            builder.HasKey("Id");

            builder.Property("Id")
                .HasColumnName("PersonEmailAddressID");

            builder.OwnsOne(_ => _.EmailAddress)
                .Property(_ => _.Value)
                    .HasColumnName("EmailAddress");
        }
    }
}