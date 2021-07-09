using AW.Services.Product.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Infrastructure.EF6.Configurations
{
    public class ProductPhotoConfiguration : EntityTypeConfiguration<ProductPhoto>
    {
        public ProductPhotoConfiguration()
        {
            ToTable("ProductPhoto");

            Property(pp => pp.Id)
                .HasColumnName("ProductPhotoId");

            Property(pp => pp.ThumbnailPhotoFileName)
                .HasMaxLength(50);

            Property(pp => pp.LargePhotoFileName)
                .HasMaxLength(50);
        }
    }
}