using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class UnitMeasureConfiguration : IEntityTypeConfiguration<Domain.UnitMeasure>
    {
        public void Configure(EntityTypeBuilder<Domain.UnitMeasure> builder)
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