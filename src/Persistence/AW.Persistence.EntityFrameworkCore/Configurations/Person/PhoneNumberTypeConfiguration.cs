using AW.Domain.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Person
{
    public class PhoneNumberTypeConfiguration : IEntityTypeConfiguration<PhoneNumberType>
    {
        public void Configure(EntityTypeBuilder<PhoneNumberType> builder)
        {
            builder.ToTable("Person.PhoneNumberType");
            builder.HasKey(pnt => pnt.Id);

            builder.Property(pnt => pnt.Id)
                .HasColumnName("PhoneNumberTypeID");

            builder.Property(pnt => pnt.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}