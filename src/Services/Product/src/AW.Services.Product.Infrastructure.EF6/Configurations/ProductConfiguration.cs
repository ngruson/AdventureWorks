using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Infrastructure.EF6.Configurations
{
    public class ProductConfiguration : EntityTypeConfiguration<Core.Entities.Product>
    {
        public ProductConfiguration()
        {
            ToTable("Product");
            HasKey(p => p.Id);

            Property(p => p.Id)
            .HasColumnName("ProductId");

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.ProductNumber)
                .IsRequired()
                .HasMaxLength(25);

            Property(p => p.Color)
                .HasMaxLength(15);

            Property(p => p.StandardCost)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(p => p.ListPrice)
                .HasPrecision(19, 4)
                .HasColumnType("money");

            Property(p => p.Size)
                .IsRequired()
                .HasMaxLength(5);

            Property(p => p.SizeUnitMeasureCode)
                .HasMaxLength(3);

            HasOptional(p => p.SizeUnitMeasure)
                .WithMany()
                .HasForeignKey(p => p.SizeUnitMeasureCode);

            Property(e => e.WeightUnitMeasureCode)
                .HasMaxLength(3);

            HasOptional(p => p.WeightUnitMeasure)
                .WithMany()
                .HasForeignKey(p => p.WeightUnitMeasureCode);

            Property(e => e.Weight)
                .HasPrecision(8, 2);

            Property(e => e.ProductLine)
                .HasMaxLength(2);

            Property(e => e.Class)
                .HasMaxLength(2);

            Property(e => e.Style)
                .HasMaxLength(2);
        }
    }
}