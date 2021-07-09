using AW.Services.ReferenceData.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.ReferenceData.Infrastructure.EF6.Configurations
{
    public class TerritoryConfiguration : EntityTypeConfiguration<Territory>
    {
        public TerritoryConfiguration()
        {
            ToTable("Territory");
            HasKey(sp => sp.Id);

            Property(sp => sp.Id)
                .HasColumnName("TerritoryID");

            Property(sp => sp.CountryRegionCode)
                .IsRequired()
                .HasMaxLength(3);

            Property(sp => sp.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(sp => sp.Group)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}