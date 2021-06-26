using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Persistence.EntityFramework.Configurations
{
    public class ProductDescriptionConfiguration : EntityTypeConfiguration<Domain.ProductDescription>
    {
        public ProductDescriptionConfiguration()
        {
            ToTable("ProductDescription");

            Property(pdc => pdc.Description)
                .IsRequired()
                .HasMaxLength(400);
        }
    }
}