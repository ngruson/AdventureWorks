using AW.Services.ReferenceData.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.ReferenceData.Infrastructure.EFCore.Configurations
{
    public class CountryRegionConfiguration : IEntityTypeConfiguration<CountryRegion>
    {
        public void Configure(EntityTypeBuilder<CountryRegion> builder)
        {
            builder.ToTable("CountryRegion");
            builder.HasKey(_ => _.CountryRegionCode);

            builder.Property(_ => _.CountryRegionCode)
                .HasMaxLength(3);

            builder.Property(_ => _.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(_ => _.StatesProvinces)
                .WithOne("CountryRegion")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}