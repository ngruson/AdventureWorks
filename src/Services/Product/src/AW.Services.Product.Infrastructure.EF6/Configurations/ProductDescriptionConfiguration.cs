using AW.Services.Product.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Infrastructure.EF6.Configurations
{
    public class ProductDescriptionConfiguration : EntityTypeConfiguration<ProductDescription>
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