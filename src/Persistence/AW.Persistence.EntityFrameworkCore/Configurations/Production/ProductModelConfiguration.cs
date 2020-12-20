using AW.Core.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class ProductModelConfiguration : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.ToTable("Production.ProductModel");

            builder.Property(pmc => pmc.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pmc => pmc.CatalogDescription)
                .HasColumnType("xml");

            builder.Property(pmc => pmc.Instructions)
                .HasColumnType("xml");
        }
    }
}