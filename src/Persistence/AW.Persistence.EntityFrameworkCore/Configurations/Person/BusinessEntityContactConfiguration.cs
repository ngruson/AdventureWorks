using AW.Domain.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Person
{
    public class BusinessEntityContactConfiguration : IEntityTypeConfiguration<BusinessEntityContact>
    {
        public void Configure(EntityTypeBuilder<BusinessEntityContact> builder)
        {
            builder.ToTable("Person.BusinessEntityContact");
            builder.HasKey(bec => new { bec.BusinessEntityID, bec.PersonID, bec.ContactTypeID });

            builder.Property(bec => bec.BusinessEntityID)
                .ValueGeneratedNever();

            builder.Property(bec => bec.PersonID)
                .ValueGeneratedNever();

            builder.Property(bec => bec.ContactTypeID)
                .ValueGeneratedNever();
        }
    }
}