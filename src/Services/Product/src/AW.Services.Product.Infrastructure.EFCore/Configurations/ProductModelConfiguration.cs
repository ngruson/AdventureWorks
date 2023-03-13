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
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("ProductModelID");

            builder.Property(_ => _.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(_ => _.CatalogDescription)
                .HasColumnType("xml");

            builder.Property(_ => _.Instructions)
                .HasColumnType("xml");
        }
    }
}
