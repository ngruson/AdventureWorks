using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class UnitMeasureConfiguration : IEntityTypeConfiguration<Core.Entities.UnitMeasure>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.UnitMeasure> builder)
        {
            builder.ToTable("UnitMeasure");
            builder.HasKey(_ => _.UnitMeasureCode);

            builder.Property(_ => _.UnitMeasureCode)
                .HasColumnName("UnitMeasureCode")
                .HasMaxLength(3);

            builder.Property(_ => _.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}