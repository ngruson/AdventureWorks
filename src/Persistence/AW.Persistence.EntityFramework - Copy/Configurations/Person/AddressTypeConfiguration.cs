using AW.Domain.Person;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class AddressTypeConfiguration : EntityTypeConfiguration<AddressType>
    {
        public AddressTypeConfiguration()
        {
            ToTable("Person.AddressType");
            HasKey(at => at.Id);

            Property(at => at.Id)
                .HasColumnName("AddressTypeID");

            Property(at => at.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}