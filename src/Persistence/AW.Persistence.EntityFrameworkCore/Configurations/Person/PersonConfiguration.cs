using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Person
{
    public class PersonConfiguration : IEntityTypeConfiguration<Core.Domain.Person.Person>
    {
        public void Configure(EntityTypeBuilder<Core.Domain.Person.Person> builder)
        {
            builder.ToTable("Person.Person");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedNever();
                
            builder.Property(p => p.PersonType)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(p => p.Title)
                .HasMaxLength(8);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.MiddleName)
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Suffix)
                .HasMaxLength(10);

            builder.Property(p => p.AdditionalContactInfo)
                .HasColumnType("xml");

            builder.Property(p => p.Demographics)
                .HasColumnType("xml");

            builder.HasMany(p => p.CreditCards);

            builder.HasMany(p => p.PhoneNumbers);
        }
    }
}