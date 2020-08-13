using AW.Domain.Person;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class PersonPhoneConfiguration : EntityTypeConfiguration<PersonPhone>
    {
        public PersonPhoneConfiguration()
        {
            ToTable("Person.PersonPhone");
            HasKey(p => new { p.BusinessEntityID, p.PhoneNumber, p.PhoneNumberTypeID });

            Property(p => p.BusinessEntityID)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.PhoneNumber)
                .HasColumnOrder(1)
                .HasMaxLength(25);

            Property(p => p.PhoneNumberTypeID)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}