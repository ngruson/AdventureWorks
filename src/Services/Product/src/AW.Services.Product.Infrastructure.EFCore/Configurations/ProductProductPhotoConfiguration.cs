using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductProductPhotoConfiguration : IEntityTypeConfiguration<Core.Entities.ProductProductPhoto>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductProductPhoto> builder)
        {
            builder.ToTable("ProductProductPhoto");
            builder.HasKey(_ => new { _.ProductId, _.ProductPhotoId });

            builder.Property(_ => _.ProductId)
                .ValueGeneratedNever();

            builder.Property(_ => _.ProductPhotoId)
                .ValueGeneratedNever();
        }
    }
}