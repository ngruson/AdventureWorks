using AW.Domain.Person;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class ContactTypeConfiguration : EntityTypeConfiguration<ContactType>
    {
        public ContactTypeConfiguration()
        {
            ToTable("Person.ContactType");

            Property(ct => ct.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}