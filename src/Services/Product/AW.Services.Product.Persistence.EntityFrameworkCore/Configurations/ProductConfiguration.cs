using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Domain.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
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

            //builder.HasMany(e => e.BillOfMaterials);

            //builder.HasMany(e => e.ProductReview)
            //    .WithOne(e => e.Product)
            //    .OnDelete(DeleteBehavior.SetNull);

            //builder.HasMany(e => e.ProductVendor)
            //    .WithOne(e => e.Product)
            //    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}