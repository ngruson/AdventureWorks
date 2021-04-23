using System.Data.Entity.ModelConfiguration;

namespace AW.Services.ReferenceData.Persistence.EntityFramework.Configurations
{
    public class StateProvinceConfiguration : EntityTypeConfiguration<Domain.StateProvince>
    {
        public StateProvinceConfiguration()
        {
            ToTable("StateProvince");
            HasKey(sp => sp.Id);

            Property(sp => sp.Id)
                .HasColumnName("StateProvinceID");

            Property(sp => sp.StateProvinceCode)
                .IsRequired()
                .HasMaxLength(3);

            Property(sp => sp.CountryRegionCode)
                .IsRequired()
                .HasMaxLength(3);

            HasRequired(sp => sp.CountryRegion)
                .WithMany(c => c.StateProvinces)
                .HasForeignKey(sp => sp.CountryRegionCode);

            Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}