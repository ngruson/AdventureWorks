using System.Data.Entity.ModelConfiguration;

namespace AW.Services.ReferenceData.Persistence.EntityFramework.Configurations
{
    public class CountryRegionConfiguration : EntityTypeConfiguration<Domain.CountryRegion>
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