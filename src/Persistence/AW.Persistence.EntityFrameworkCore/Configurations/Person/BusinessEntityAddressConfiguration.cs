using AW.Domain.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Person
{
    public class BusinessEntityAddressConfiguration : IEntityTypeConfiguration<BusinessEntityAddress>
    {
        public void Configure(EntityTypeBuilder<BusinessEntityAddress> builder)
        {
            builder.ToTable("Person.BusinessEntityAddress");
            builder.HasKey(bea => new { bea.BusinessEntityID, bea.AddressID, bea.AddressTypeID });

            builder.Property(bea => bea.BusinessEntityID)
                .ValueGeneratedNever();

            builder.Property(bea => bea.AddressID)
                .ValueGeneratedNever();

            builder.Property(bea => bea.AddressTypeID)
                .ValueGeneratedNever();
        }
    }
}