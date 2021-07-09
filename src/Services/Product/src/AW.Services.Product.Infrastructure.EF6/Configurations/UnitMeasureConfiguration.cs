using AW.Services.Product.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Infrastructure.EF6.Configurations
{
    public class UnitMeasureConfiguration : EntityTypeConfiguration<UnitMeasure>
    {
        public UnitMeasureConfiguration()
        {
            ToTable("UnitMeasure");
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