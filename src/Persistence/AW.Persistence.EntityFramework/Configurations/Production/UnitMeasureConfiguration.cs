using AW.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class UnitMeasureConfiguration : EntityTypeConfiguration<UnitMeasure>
    {
        public UnitMeasureConfiguration()
        {
            ToTable("Production.UnitMeasure");
            HasKey(um => um.UnitMeasureCode);

            Property(um => um.UnitMeasureCode)
                .HasColumnName("UnitMeasureCode")
                .HasMaxLength(3);

            Property(um => um.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}