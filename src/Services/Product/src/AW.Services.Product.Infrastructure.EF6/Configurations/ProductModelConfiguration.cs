using AW.Services.Product.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AW.Services.Product.Infrastructure.EF6.Configurations
{
    public class ProductModelConfiguration : EntityTypeConfiguration<ProductModel>
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