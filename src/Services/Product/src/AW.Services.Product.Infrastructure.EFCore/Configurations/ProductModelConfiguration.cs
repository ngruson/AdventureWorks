using AW.Services.Product.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductModelConfiguration : IEntityTypeConfiguration<Core.Entities.ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.ToTable("ProductModel");
            builder.HasKey("Id");

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