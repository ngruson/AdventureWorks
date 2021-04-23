using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Persistence.EntityFramework.Configurations
{
    public class ProductPhotoConfiguration : EntityTypeConfiguration<Domain.ProductPhoto>
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