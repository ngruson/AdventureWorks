using AW.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class UnitMeasureConfiguration : IEntityTypeConfiguration<UnitMeasure>
    {
        public void Configure(EntityTypeBuilder<UnitMeasure> builder)
        {
            builder.ToTable("Production.UnitMeasure");
            builder.HasKey(um => um.Id);

            builder.Property(um => um.Id)
                .HasColumnName("UnitMeasureCode")
                .HasMaxLength(3);

            builder.Property(um => um.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}