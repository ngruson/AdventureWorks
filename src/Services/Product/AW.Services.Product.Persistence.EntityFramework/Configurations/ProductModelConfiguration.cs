using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Persistence.EntityFramework.Configurations
{
    public class ProductModelConfiguration : EntityTypeConfiguration<Domain.ProductModel>
    {
        public ProductModelConfiguration()
        {
            ToTable("ProductModel");

            Property(pmc => pmc.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(pmc => pmc.CatalogDescription)
                .HasColumnType("xml");

            Property(pmc => pmc.Instructions)
                .HasColumnType("xml");
        }
    }
}