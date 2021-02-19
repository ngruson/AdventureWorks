using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class ProductDescriptionConfiguration : IEntityTypeConfiguration<Domain.ProductDescription>
    {
        public void Configure(EntityTypeBuilder<Domain.ProductDescription> builder)
        {
            builder.ToTable("ProductDescription");

            builder.Property(pdc => pdc.Description)
                .IsRequired()
                .HasMaxLength(400);
        }
    }
}