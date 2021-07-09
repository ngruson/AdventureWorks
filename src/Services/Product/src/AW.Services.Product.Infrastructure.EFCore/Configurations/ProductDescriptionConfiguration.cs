using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductDescriptionConfiguration : IEntityTypeConfiguration<Core.Entities.ProductDescription>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductDescription> builder)
        {
            builder.ToTable("ProductDescription");

            builder.Property(pdc => pdc.Description)
                .IsRequired()
                .HasMaxLength(400);
        }
    }
}