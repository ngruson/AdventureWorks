using System.Data.Entity.ModelConfiguration;

namespace AW.Services.SalesOrder.Persistence.EntityFramework.Configurations
{
    public class PersonConfiguration : EntityTypeConfiguration<Domain.Person>
    {
        public PersonConfiguration()
        {
            ToTable("Person");
            HasKey(p => p.Id);
        }
    }
}