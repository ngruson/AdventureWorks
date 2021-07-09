using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class UnitMeasureConfiguration : IEntityTypeConfiguration<Core.Entities.UnitMeasure>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.UnitMeasure> builder)
        {
            builder.ToTable("UnitMeasure");
            builder.HasKey(um => um.UnitMeasureCode);

            builder.Property(um => um.UnitMeasureCode)
                .HasColumnName("UnitMeasureCode")
                .HasMaxLength(3);

            builder.Property(um => um.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}