using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW.Services.Product.Persistence.EntityFrameworkCore.Configurations
{
    public class ProductPhotoConfiguration : IEntityTypeConfiguration<Domain.ProductPhoto>
    {
        public void Configure(EntityTypeBuilder<Domain.ProductPhoto> builder)
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