using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Persistence.EntityFramework.Configurations
{
    public class UnitMeasureConfiguration : EntityTypeConfiguration<Domain.UnitMeasure>
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