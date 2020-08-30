using AW.Domain.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Person
{
    public class PersonPhoneConfiguration : IEntityTypeConfiguration<PersonPhone>
    {
        public void Configure(EntityTypeBuilder<PersonPhone> builder)
        {
            builder.ToTable("Person.PersonPhone");
            builder.HasKey(p => new { p.Id, p.PhoneNumber, p.PhoneNumberTypeID });

            builder.Property(p => p.Id)
                .HasColumnName("BusinessEntityID")
                .ValueGeneratedNever();

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(25);

            builder.Property(p => p.PhoneNumberTypeID)
                .ValueGeneratedNever();
        }
    }
}