using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Core.Entities.Product>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey("Id");

            builder.Property("Id")
                .HasColumnName("ProductId");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.ProductNumber)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(p => p.Color)
                .HasMaxLength(15);

            builder.Property(p => p.StandardCost)
                .HasColumnType("decimal(19,4)")
                .HasColumnType("money");

            builder.Property(p => p.ListPrice)
                .HasColumnType("decimal(19,4)")
                .HasColumnType("money");

            builder.Property(p => p.Size)
                .HasMaxLength(5);

            builder.Property(p => p.SizeUnitMeasureCode)
                .HasMaxLength(3);

            builder.HasOne(p => p.SizeUnitMeasure)
                .WithMany()
                .HasForeignKey(p => p.SizeUnitMeasureCode);

            builder.Property(e => e.WeightUnitMeasureCode)
                .HasMaxLength(3);

            builder.HasOne(p => p.WeightUnitMeasure)
                .WithMany()
                .HasForeignKey(p => p.WeightUnitMeasureCode);

            builder.Property(e => e.Weight)
                .HasColumnType("decimal(8,2)");

            builder.Property(e => e.ProductLine)
                .HasMaxLength(2);

            builder.Property(e => e.Class)
                .HasMaxLength(2);

            builder.Property(e => e.Style)
                .HasMaxLength(2);
        }
    }
}