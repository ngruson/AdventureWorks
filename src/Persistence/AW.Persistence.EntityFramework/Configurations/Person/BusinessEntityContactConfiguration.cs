using AW.Core.Domain.Person;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class BusinessEntityContactConfiguration : EntityTypeConfiguration<BusinessEntityContact>
    {
        public BusinessEntityContactConfiguration()
        {
            ToTable("Person.BusinessEntityContact");
            HasKey(bec => new { bec.BusinessEntityID, bec.PersonID, bec.ContactTypeID });

            Property(bec => bec.BusinessEntityID)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(bec => bec.PersonID)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(bec => bec.ContactTypeID)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}