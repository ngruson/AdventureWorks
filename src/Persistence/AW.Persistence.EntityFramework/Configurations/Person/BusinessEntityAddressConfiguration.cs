using AW.Domain.Person;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class BusinessEntityAddressConfiguration : EntityTypeConfiguration<BusinessEntityAddress>
    {
        public BusinessEntityAddressConfiguration()
        {
            ToTable("Person.BusinessEntityAddress");
            HasKey(bea => new { bea.BusinessEntityID, bea.AddressID, bea.AddressTypeID });

            Property(bea => bea.BusinessEntityID)
                .HasColumnName("BusinessEntityID")
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(bea => bea.AddressID)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(bea => bea.AddressTypeID)
                .HasColumnOrder(2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}