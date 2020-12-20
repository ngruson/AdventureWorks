using AW.Core.Domain.Person;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class PhoneNumberTypeConfiguration : EntityTypeConfiguration<PhoneNumberType>
    {
        public PhoneNumberTypeConfiguration()
        {
            ToTable("Person.PhoneNumberType");
            HasKey(pnt => pnt.Id);

            Property(pnt => pnt.Id)
                .HasColumnName("PhoneNumberTypeID");

            Property(pnt => pnt.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}