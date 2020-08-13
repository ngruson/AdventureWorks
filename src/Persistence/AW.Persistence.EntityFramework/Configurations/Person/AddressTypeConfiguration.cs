using AW.Domain.Person;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class AddressTypeConfiguration : EntityTypeConfiguration<AddressType>
    {
        public AddressTypeConfiguration()
        {
            ToTable("Person.AddressType");

            Property(at => at.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}