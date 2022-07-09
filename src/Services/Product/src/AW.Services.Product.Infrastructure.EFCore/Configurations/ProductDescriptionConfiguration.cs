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
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Description)
                .IsRequired()
                .HasMaxLength(400);
        }
    }
}