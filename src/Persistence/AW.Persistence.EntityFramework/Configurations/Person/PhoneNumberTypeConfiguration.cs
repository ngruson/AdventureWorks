using AW.Domain.Person;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class PhoneNumberTypeConfiguration : EntityTypeConfiguration<PhoneNumberType>
    {
        public PhoneNumberTypeConfiguration()
        {
            ToTable("Person.PhoneNumberType");

            Property(pnt => pnt.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}