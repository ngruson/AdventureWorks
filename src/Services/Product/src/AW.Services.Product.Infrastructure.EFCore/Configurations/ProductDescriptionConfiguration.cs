using AW.Services.Product.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductDescriptionConfiguration : IEntityTypeConfiguration<Core.Entities.ProductDescription>
    {
        public void Configure(EntityTypeBuilder<ProductDescription> builder)
        {
            builder.ToTable("ProductDescription");
            builder.HasKey("Id");

            builder.Property(pdc => pdc.Description)
                .IsRequired()
                .HasMaxLength(400);
        }
    }
}