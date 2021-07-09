using AW.Services.Customer.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Customer.Infrastructure.EF6.Configurations
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
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