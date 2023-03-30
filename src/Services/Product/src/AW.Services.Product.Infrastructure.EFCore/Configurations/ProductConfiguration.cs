using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Core.Entities.Product>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("ProductId");

            builder.Property(_ => _.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(_ => _.ProductNumber)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(_ => _.Color)
                .HasMaxLength(15);

            builder.Property(_ => _.StandardCost)
                .HasColumnType("decimal(19,4)")
                .HasColumnType("money");

            builder.Property(_ => _.ListPrice)
                .HasColumnType("decimal(19,4)")
                .HasColumnType("money");

            builder.Property(_ => _.Size)
                .HasMaxLength(5);

            builder.Property(_ => _.SizeUnitMeasureCode)
                .HasMaxLength(3);

            builder.HasOne(_ => _.SizeUnitMeasure)
                .WithMany()
                .HasForeignKey(_ => _.SizeUnitMeasureCode);

            builder.Property(_ => _.WeightUnitMeasureCode)
                .HasMaxLength(3);

            builder.HasOne(_ => _.WeightUnitMeasure)
                .WithMany()
                .HasForeignKey(_ => _.WeightUnitMeasureCode);

            builder.Property(_ => _.Weight)
                .HasColumnType("decimal(8,2)");

            builder.Property(_ => _.ProductLine)
                .HasMaxLength(2);

            builder.Property(_ => _.Class)
                .HasMaxLength(2);

            builder.Property(_ => _.Style)
                .HasMaxLength(2);
        }
    }
}
