using AW.Core.Domain.Person;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class EmailAddressConfiguration : EntityTypeConfiguration<EmailAddress>
    {
        public EmailAddressConfiguration()
        {
            ToTable("Person.EmailAddress");
            HasKey(ea => new { ea.Id, ea.EmailAddressID });

            Property(ea => ea.Id)
                .HasColumnName("BusinessEntityID")
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(ea => ea.EmailAddressID)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(ea => ea.EmailAddress1)
                .HasColumnName("EmailAddress")
                .HasMaxLength(50);
        }
    }
}