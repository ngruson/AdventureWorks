using AW.Domain.Person;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class PersonConfiguration : EntityTypeConfiguration<Domain.Person.Person>
    {
        public PersonConfiguration()
        {
            ToTable("Person.Person");
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName("BusinessEntityID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.PersonType)
                .IsRequired()
                .HasMaxLength(2);

            Property(p => p.Title)
                .HasMaxLength(8);

            Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.MiddleName)
                .HasMaxLength(50);

            Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.Suffix)
                .HasMaxLength(10);

            Property(p => p.AdditionalContactInfo)
                .HasColumnType("xml");

            Property(p => p.Demographics)
                .HasColumnType("xml");

            HasMany(p => p.CreditCards);

            HasMany(p => p.PhoneNumbers);
        }
    }
}