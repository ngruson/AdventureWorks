using AW.Services.ReferenceData.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.ReferenceData.Infrastructure.EF6.Configurations
{
    public class CountryRegionConfiguration : EntityTypeConfiguration<CountryRegion>
    {
        public CountryRegionConfiguration()
        {
            ToTable("CountryRegion");
            HasKey(cr => cr.CountryRegionCode);

            Property(cr => cr.CountryRegionCode)
                .HasMaxLength(3);

            Property(cr => cr.Name)
                .IsRequired()
                .HasMaxLength(50);

            HasMany(e => e.StateProvinces)
                .WithRequired(e => e.CountryRegion);
        }
    }
}