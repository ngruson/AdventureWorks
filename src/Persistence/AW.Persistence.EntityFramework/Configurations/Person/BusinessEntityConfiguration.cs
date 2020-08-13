using AW.Domain.Person;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class BusinessEntityConfiguration : EntityTypeConfiguration<BusinessEntity>
    {
        public BusinessEntityConfiguration()
        {
            ToTable("Person.BusinessEntity");
        }
    }
}