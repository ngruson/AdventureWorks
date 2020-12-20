using AW.Core.Domain.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Person
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Person.Address");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("AddressID");

            builder.Property(a => a.AddressLine1)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(a => a.AddressLine2)
                .HasMaxLength(60);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(a => a.PostalCode)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}