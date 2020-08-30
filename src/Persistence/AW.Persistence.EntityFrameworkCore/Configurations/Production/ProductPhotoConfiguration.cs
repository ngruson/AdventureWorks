using AW.Domain.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Persistence.EntityFrameworkCore.Configurations.Production
{
    public class ProductPhotoConfiguration : IEntityTypeConfiguration<ProductPhoto>
    {
        public void Configure(EntityTypeBuilder<ProductPhoto> builder)
        {
            builder.ToTable("Production.ProductPhoto");

            builder.Property(pp => pp.Id)
                .HasColumnName("ProductPhotoID");

            builder.Property(pp => pp.ThumbnailPhotoFileName)
                .HasMaxLength(50);

            builder.Property(pp => pp.LargePhotoFileName)
                .HasMaxLength(50);
        }
    }
}