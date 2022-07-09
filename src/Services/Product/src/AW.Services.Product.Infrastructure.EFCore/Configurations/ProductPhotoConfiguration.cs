using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductPhotoConfiguration : IEntityTypeConfiguration<Core.Entities.ProductPhoto>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductPhoto> builder)
        {
            builder.ToTable("ProductPhoto");
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("ProductPhotoID");

            builder.Property(_ => _.ThumbnailPhotoFileName)
                .HasMaxLength(50);

            builder.Property(_ => _.LargePhotoFileName)
                .HasMaxLength(50);
        }
    }
}