using AW.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class ProductPhotoConfiguration : EntityTypeConfiguration<ProductPhoto>
    {
        public ProductPhotoConfiguration()
        {
            ToTable("Production.ProductPhoto");

            Property(pp => pp.Id)
                .HasColumnName("ProductPhotoID");

            Property(pp => pp.ThumbnailPhotoFileName)
                .HasMaxLength(50);

            Property(pp => pp.LargePhotoFileName)
                .HasMaxLength(50);
        }
    }
}