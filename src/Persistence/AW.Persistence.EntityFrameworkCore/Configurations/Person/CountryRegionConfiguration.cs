using AW.Core.Domain.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Person
{
    public class CountryRegionConfiguration : IEntityTypeConfiguration<CountryRegion>
    {
        public void Configure(EntityTypeBuilder<CountryRegion> builder)
        {
            builder.ToTable("Person.CountryRegion");
            builder.HasKey(cr => cr.CountryRegionCode);

            builder.Property(cr => cr.CountryRegionCode)
                .HasMaxLength(3);

            builder.Property(cr => cr.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(e => e.StateProvinces)
                .WithOne(e => e.CountryRegion)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}