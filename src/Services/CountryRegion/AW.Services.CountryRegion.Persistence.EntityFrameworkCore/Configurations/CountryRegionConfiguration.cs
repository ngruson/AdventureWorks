using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.CountryRegion.Persistence.EntityFrameworkCore.Configurations
{
    public class CountryRegionConfiguration : IEntityTypeConfiguration<Domain.CountryRegion>
    {
        public void Configure(EntityTypeBuilder<Domain.CountryRegion> builder)
        {
            builder.ToTable("CountryRegion");
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