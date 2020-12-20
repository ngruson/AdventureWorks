using AW.Core.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class ProductDescriptionConfiguration : EntityTypeConfiguration<ProductDescription>
    {
        public ProductDescriptionConfiguration()
        {
            ToTable("Production.ProductDescription");

            Property(pdc => pdc.Description)
                .IsRequired()
                .HasMaxLength(400);
        }   
    }
}