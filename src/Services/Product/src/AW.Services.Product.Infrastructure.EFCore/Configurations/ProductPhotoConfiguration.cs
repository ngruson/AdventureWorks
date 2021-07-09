using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Infrastructure.EFCore.Configurations
{
    public class ProductPhotoConfiguration : IEntityTypeConfiguration<Core.Entities.ProductPhoto>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.ProductPhoto> builder)
        {
            builder.ToTable("ProductPhoto");

            builder.Property(pd => pd.Id)
                .HasColumnName("ProductPhotoID");

            builder.Property(pp => pp.ThumbnailPhotoFileName)
                .HasMaxLength(50);

            builder.Property(pp => pp.LargePhotoFileName)
                .HasMaxLength(50);
        }
    }
}