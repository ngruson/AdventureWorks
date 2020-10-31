using AW.Domain.Person;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class BusinessEntityConfiguration : EntityTypeConfiguration<BusinessEntity>
    {
        public BusinessEntityConfiguration()
        {
            ToTable("Person.BusinessEntity");
            HasKey(s => s.Id);

            Property(s => s.Id)
                .HasColumnName("BusinessEntityID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}