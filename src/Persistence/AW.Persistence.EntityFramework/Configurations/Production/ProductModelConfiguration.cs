using AW.Core.Domain.Production;
using System.Data.Entity.ModelConfiguration;

namespace AW.Persistence.EntityFramework.Configurations.Production
{
    public class ProductModelConfiguration : EntityTypeConfiguration<ProductModel>
    {
        public ProductModelConfiguration()
        {
            ToTable("Production.ProductModel");

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