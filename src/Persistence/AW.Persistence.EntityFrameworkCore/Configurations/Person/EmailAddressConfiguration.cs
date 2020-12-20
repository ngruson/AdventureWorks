using AW.Core.Domain.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Person
{
    public class EmailAddressConfiguration : IEntityTypeConfiguration<EmailAddress>
    {
        public void Configure(EntityTypeBuilder<EmailAddress> builder)
        {
            builder.ToTable("Person.EmailAddress");
            builder.HasKey(ea => new { ea.Id, ea.EmailAddressID });

            builder.Property(ea => ea.Id)
                .HasColumnName("BusinessEntityID")
                .ValueGeneratedNever();

            builder.Property(ea => ea.EmailAddress1)
                .HasColumnName("EmailAddress")
                .HasMaxLength(50);
        }
    }
}