using AW.Domain.Person;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class ContactTypeConfiguration : EntityTypeConfiguration<ContactType>
    {
        public ContactTypeConfiguration()
        {
            ToTable("Person.ContactType");
            HasKey(ct => ct.Id);

            Property(ct => ct.Id)
                .HasColumnName("ContactTypeID");

            Property(ct => ct.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}