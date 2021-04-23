using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Persistence.EntityFramework.Configurations
{
    public class PersonConfiguration : EntityTypeConfiguration<Domain.Person>
    {
        public PersonConfiguration()
        {
            ToTable("Person");
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnName("PersonID");
        }
    }
}