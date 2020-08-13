using AW.Domain.Person;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Person
{
    public class CountryRegionConfiguration : EntityTypeConfiguration<CountryRegion>
    {
        public CountryRegionConfiguration()
        {
            ToTable("Person.CountryRegion");
            HasKey(cr => cr.CountryRegionCode);

            Property(cr => cr.CountryRegionCode)
                .HasMaxLength(3);

            Property(cr => cr.Name)
                .IsRequired()
                .HasMaxLength(50);

            HasMany(e => e.StateProvinces)
                .WithRequired(e => e.CountryRegion)
                .WillCascadeOnDelete(false);
        }
    }
}